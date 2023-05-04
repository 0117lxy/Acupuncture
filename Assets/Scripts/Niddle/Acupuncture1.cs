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

    private float _OverTime = 0f;//��ͣʱ��
    private bool _EnterAnchor = false;//�Ƿ���ͣ��Ѩλ��
    //private Button _AimAnchor;//��ͣ��Button
    private GameObject _AimAnchor;//��ͣ��Button

    ClickToInstantiate _ClickToInstantiate;

    public float _RotateSpeed = 3.0f;
    private Quaternion _InitialNiddleRotation;//��ʼ��ת
    private Quaternion _AsistNiddleRotation;//������ת
    public Vector3 _LastNiddleRotation;//������ת

    private bool _IsButtonDown;//�Ƿ������
    public float _MoveSpeed = 1.0f;
    public float _MaxMoveDis = 3;
    private float _MoveTime = 0f;
    private Vector3 _InitialNiddlePos;//��ʼλ��

    Vector3 _AimAnchorPos;

    //ָ��ѡ������ر���
    public GameObject _BezierPrefab;
    public GameObject _BezierObject;
    public Vector3 _BezierPos;
    public Quaternion _BezierRotation;
    private bool _IsBuildBezier = false;
    public GameObject _Canvas;

    //����ָʾ�������Э��
    Coroutine _DestroyCoroutine;

    //������ر���
    public float _AcupunctureSpeed = 1.0f;
    public bool _IsAcupuncture = false;//�Ƿ��������
    float _NiddleHeight;//��ĸ߶�
    float _AcupunctureTime = 0f;
    public float _AcupunctureTotalTime = 1f;

    //���ڱ�ʾ��������е�״̬���ֱ��������롢��׼�����������롢û״̬
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
            Debug.Log("����Anchor��");
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

    //��׼Anchor
    void AimAnchor()
    {
        if(_EnterAnchor == true)
        {
            //��Ŀ������֮�����
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

    //����Bezier
    void BuildBezier()
    {
        _BezierObject = Instantiate(_BezierPrefab, _BezierPos, _BezierRotation);

        _IsBuildBezier = true;
    }

    //���ٱ������ƶ����壬�����Ѿ����������������boolֵΪfalse
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
