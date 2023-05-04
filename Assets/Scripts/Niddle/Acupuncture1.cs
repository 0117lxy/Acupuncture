using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Acupuncture1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    /*public enum AcupunctureState
    {
        Idle,
        PickUp,
        Aim,
        Charge,
        Insert
    }*/

    private float _OverTime = 0f;//悬停时间
    private bool _EnterAnchor = false;//是否悬停在穴位上
    //private Button _AimAnchor;//悬停的Button
    private GameObject _AimAnchor;//悬停的Button

    ClickToInstantiate _ClickToInstantiate;

    public float _RotateSpeed = 3.0f;
    private Quaternion _InitialNiddleRotation;//初始旋转
    private Quaternion _AsistNiddleRotation;//辅助旋转
    public Vector3 _LastNiddleRotation;//最终旋转

    private bool _IsButtonDown;//是否点击鼠标
    public float _MoveSpeed = 1.0f;
    public float _MaxMoveDis = 3;
    private float _MoveTime = 0f;
    private Vector3 _InitialNiddlePos;//初始位置

    Vector3 _AimAnchorPos;

    //指针选择器相关变量
    public GameObject _BezierPrefab;
    public GameObject _BezierObject;
    public Vector3 _BezierPos;
    public Quaternion _BezierRotation;
    private bool _IsBuildBezier = false;
    public GameObject _Canvas;

    //销毁指示器和针的协程
    Coroutine _DestroyCoroutine;

    //下针相关变量
    public float _AcupunctureSpeed = 1.0f;
    public bool _IsAcupuncture = false;//是否可以下针
    float _NiddleHeight;//针的高度
    float _AcupunctureTime = 0f;
    public float _AcupunctureTotalTime = 1f;

    //用于表示下针过程中的状态，分别是拿起针、对准、蓄力、下针、没状态
    public enum AcupunctureState
    {
        Bring,
        Focus,
        FocusOver,
        Strengthen,
        StrengthenOver,
        Niddling,
        Nonesense
    }

    public AcupunctureState _State;

    private void Start()
    {
        _ClickToInstantiate = GameObject.Find("NiddleType").GetComponent<ClickToInstantiate>();

        _Canvas = GameObject.Find("Canvas");

        _State = AcupunctureState.Nonesense;
    }

    private void Update()
    {
        switch(_State)
        {
            case AcupunctureState.Bring:
                break;
            case AcupunctureState.Focus:
                break;
            case AcupunctureState.FocusOver:
                break;
            case AcupunctureState.Strengthen:
                break;
            case AcupunctureState.StrengthenOver:
                break;
            case AcupunctureState.Niddling:
                break;
            case AcupunctureState.Nonesense:
                break;
            default:
                break;
        }
    }

/*    void ChangeAcupunctureState()
    {
        if(_ClickToInstantiate._IsSpawn == true && _State == AcupunctureState.Nonesense)
        {
            _State = AcupunctureState.Bring;
            Debug.Log("_State: " + _State);
        }

        if (_EnterAnchor == false && _ClickToInstantiate._IsSpawn == true)
        {
            _State = AcupunctureState.Bring;
            Debug.Log("_State: " + _State);
        }

        if (_AimAnchor != null && _EnterAnchor == true && _State == AcupunctureState.Bring)
        {
            _State = AcupunctureState.Focus;
            Debug.Log("_State: " + _State);
        }
        
        if(_IsBuildBezier == true && _State == AcupunctureState.Focus)
        {
            _State = AcupunctureState.FocusOver;
            Debug.Log("_State: " + _State);
        }

        if(_IsButtonDown == true && _State == AcupunctureState.FocusOver)
        {
            _State = AcupunctureState.Strengthen;
            Debug.Log("_State: " + _State);
        }
        
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.CompareTag("Anchor") && _State == AcupunctureState.Bring)
        {
            _AimAnchor = this.gameObject;
            _EnterAnchor = true;
            _State = AcupunctureState.Focus;
            _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            _InitialNiddleRotation = _ClickToInstantiate._NiddleObject.transform.rotation;
            Debug.Log("进入Anchor！");
            Debug.Log("_AimAnchor: " + _AimAnchor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AimAnchorExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_State == AcupunctureState.FocusOver)
        {
            _MoveTime = 0f;
            _IsButtonDown = true;
            _ClickToInstantiate._IsAcupuncture = true;
            _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _MoveTime = 0f;
        _IsButtonDown = false;
        _ClickToInstantiate._IsAcupuncture = false;
        _ClickToInstantiate._NiddleObject.transform.position = _InitialNiddlePos;
        DestroyBezierObject();
        _State = AcupunctureState.Strengthen;
    }

    void OverTimeUp()
    {
        if(_State == AcupunctureState.Focus)
        {
            _OverTime += Time.deltaTime;
        }

        if (_OverTime < 1.0f)
        {
            AimAnchor();
        }

        if (_OverTime >= 1.0f && _IsBuildBezier == false)
        {
            BuildBezier();
        }
    }

    //对准Anchor
    void AimAnchor()
    {
        if(_EnterAnchor == true)
        {
            //画目标与针之间的线
            Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position);
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            Debug.DrawLine(niddlePos, aimAnchorPos, Color.red);

            _ClickToInstantiate._NiddleObject.transform.rotation = Quaternion.Slerp(_ClickToInstantiate._NiddleObject.transform.rotation, Quaternion.Euler(_LastNiddleRotation), _RotateSpeed * Time.deltaTime);
        }
    }

    void AimAnchorExit()
    {
        if(_EnterAnchor == true)
        {
            if(_ClickToInstantiate._NiddleObject != null)
            {
                _ClickToInstantiate._NiddleObject.transform.rotation = _InitialNiddleRotation;
            }

            _ClickToInstantiate._IsAcupuncture = false;
            DestroyBezierObject();
            _OverTime = 0f;
            _EnterAnchor = false;
        }
    }

    //创建Bezier
    void BuildBezier()
    {
        _BezierObject = Instantiate(_BezierPrefab, _BezierPos, _BezierRotation);

        _IsBuildBezier = true;
    }

    //销毁贝塞尔移动物体，设置已经创建贝塞尔物体的bool值为false
    public void DestroyBezierObject()
    {
        if (_BezierObject != null)
        {
            Destroy(_BezierObject);
            _IsBuildBezier = false;

            if (_DestroyCoroutine != null)
            {
                StopCoroutine(_DestroyCoroutine);
                _DestroyCoroutine = null;
            }
        }
    }

    void Strengthen()
    {
        if(_OverTime > 1f && _IsButtonDown == true && _MoveTime < 1f)
        {

        }
    }
}
