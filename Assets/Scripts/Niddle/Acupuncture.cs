using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Acupuncture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
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

    //提示信息相关变量
    /*public int _TipNum;
    public bool[] _IsTip;*/

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

    void Start()
    {
        _ClickToInstantiate = GameObject.Find("NiddleType").GetComponent<ClickToInstantiate>();

        _Canvas = GameObject.Find("Canvas");

        _State = AcupunctureState.Nonesense;

        /*_TipNum = 4;
        _TipNum++;//多设置一个最后一位，用于把所有的提示信息都隐藏
        _IsTip = new bool[_TipNum];
        for (int i=0; i< _TipNum; i++)
        {
            _IsTip[i] = false;
        }*/
    }

    void Update()
    {

        ChangeAcupunctureState();

        OverTimeUp(_EnterAnchor);
        AimAnchor(_EnterAnchor);
        ToAcupuncture();
    }

    public void ChangeAcupunctureState()
    {
        if(_ClickToInstantiate._IsSpawn == true && _State == AcupunctureState.Nonesense)
        {
            _State = AcupunctureState.Bring;
        }
    }

    //鼠标指针悬在正确的button上，记录一下最初的旋转和位置，用于鼠标移出button后针的参数的恢复
    //设置进入button的bool值为true，用于AimAnchor()函数是否进行针的对准以及下针的条件的判断
    public void OnPointerEnter(PointerEventData eventData)
    {
        //_AimAnchor = gameObject.GetComponent<Button>();
        _AimAnchor = this.gameObject;
        if(_AimAnchor != null && _AimAnchor.tag == "Anchor" && _State == AcupunctureState.Bring)
        {        
                //_State = AcupunctureState.Bring;
                //_NiddleHeight = _ClickToInstantiate._NiddleHeight;
                _EnterAnchor = true;
                _InitialNiddleRotation = _ClickToInstantiate._NiddleRotation;
                _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;
        }
    }

    //鼠标指针移出button就恢复针的初始旋转，把悬浮时间也置零
    public void OnPointerExit(PointerEventData eventData)
    {
        AimAnchorExit(_EnterAnchor);
        _OverTime = 0f;
        //_EnterAnchor = false;
    }

    //当鼠标点击时，初始化针蓄力后移时间，记录点击的bool值为true，设置正在下针，记录初始位置
    public void OnPointerDown(PointerEventData eventData)
    {
        if(_ClickToInstantiate._IsSpawn == true)
        {
            _MoveTime = 0f;
            _IsButtonDown = true;
            _ClickToInstantiate._IsAcupuncture = true;
            _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;
        } 
    }

    //当鼠标松开的时候，重置蓄力后移时间，设置点击的bool值为false，重置针的位置，销毁贝塞尔移动物体
    public void OnPointerUp(PointerEventData eventData)
    {
        _MoveTime = 0f;
        _IsButtonDown = false;
        //_ClickToInstantiate._NiddleObject.transform.position = _InitialNiddlePos;
        DestroyNiddle();
        DestroyBezierObject();

    }

    //_EnterAnchor为true的时候_OverTime逐帧增加
    void OverTimeUp(bool _EnterAnchor)
    {
        if(_EnterAnchor == true)
        {
            if(_OverTime < 2f)
            {
                _OverTime += Time.deltaTime;
            }
        }
    }

    //对准穴位的函数
    void AimAnchor(bool _EnterAnchor)
    {
        if(_EnterAnchor == true && _AimAnchor.tag =="Anchor" && _ClickToInstantiate._NiddleObject != null)
        {
            //画目标与针之间的线
            Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position); 
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            Debug.DrawLine(niddlePos, aimAnchorPos, Color.red);

            //对准
            if (_OverTime < 1.0f)
            {
                if(_State == AcupunctureState.Bring)
                {
                    _State = AcupunctureState.Focus;
                }
                

                _ClickToInstantiate._NiddleObject.transform.rotation = Quaternion.Slerp(_ClickToInstantiate._NiddleObject.transform.rotation, Quaternion.Euler(_LastNiddleRotation), _RotateSpeed * Time.deltaTime);
            }
            else if(_OverTime > 1.2f)
            {
                if (!_IsBuildBezier)
                {
                    //_IsTip[2] = true;

                    _BezierObject = Instantiate(_BezierPrefab, _BezierPos, _BezierRotation);
                    
                    _IsBuildBezier = true;
                }
            }
            else
            {
                if(_State == AcupunctureState.Focus)
                {
                    _State = AcupunctureState.FocusOver;
                }
                
            }

            //鼠标点击时，沿针的y轴正向上移
            if(_OverTime > 1f && _IsButtonDown == true && _MoveTime < 1f)
            {
                if(_State == AcupunctureState.FocusOver)
                {
                    _State = AcupunctureState.Strengthen;
                }     

                _MoveTime += Time.deltaTime;
                Vector3 moveDir = _ClickToInstantiate._NiddleObject.transform.up;
                _ClickToInstantiate._NiddleObject.transform.position += moveDir * _MoveSpeed * Time.deltaTime;
                
                if(_IsBuildBezier)
                {
                    _BezierObject.GetComponent<BezierMove>()._TimeToMove = true;
                }
            }
            else if(_OverTime > 1f && _IsButtonDown == true && _MoveTime > 1f)
            {
                //_IsTip[3] = true;
                if(_State == AcupunctureState.Strengthen)
                {
                    _State = AcupunctureState.StrengthenOver;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //_IsTip[4] = true;

                    _IsAcupuncture = true;
                    if(_State == AcupunctureState.StrengthenOver)
                    {
                        _State = AcupunctureState.Niddling;
                    }
                    
                    if (_IsBuildBezier == true)
                    {
                        _BezierObject.GetComponent<BezierMove>()._TimeToMove = false;
                    }
                }
            }
        }
    }

    //退出对准，重置针的旋转为初始旋转
    void AimAnchorExit(bool _EnterAnchor)
    {
        //判断_EnterAnchor为true的意义在于有可能鼠标可能放在别的UI上，如果不判断可能会出错
        if(_EnterAnchor == true)
        {
            if(_ClickToInstantiate._NiddleObject != null)
            {
                _ClickToInstantiate._NiddleObject.transform.rotation = _InitialNiddleRotation;
            } 
            _ClickToInstantiate._IsAcupuncture = false;
            this._EnterAnchor = false;
            _State = AcupunctureState.Bring;
            DestroyBezierObject();
        } 
    }
    
    //销毁贝塞尔移动物体，设置已经创建贝塞尔物体的bool值为false
    public void DestroyBezierObject()
    {
        if(_BezierObject != null)
        {
            Destroy(_BezierObject);
            _IsBuildBezier = false;

            if (_DestroyCoroutine != null)
            {
                StopCoroutine(_DestroyCoroutine);
            }
        }
            
    }

    //当指针对准之后就可以根据贝塞尔移动物体的指针的位置进行下针，根据指针的位置判断下针是否正确
    void ToAcupuncture()
    {
        if(_IsAcupuncture == true && _AcupunctureTime < _AcupunctureTotalTime)
        {
            _AcupunctureTime += Time.deltaTime;
            Debug.Log("正在下针：" + _AcupunctureTime);

            //Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position);
            Vector3 aimAnchorPos = _AimAnchor.transform.position;

            Debug.Log("穴位位置：" + aimAnchorPos);
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            Debug.Log("针位置：" + niddlePos);
            Vector3 dir = aimAnchorPos - niddlePos;
            Debug.Log("方向：" + dir);
            Debug.DrawLine(niddlePos, aimAnchorPos);
            _ClickToInstantiate._NiddleObject.transform.position += dir * _AcupunctureSpeed * Time.deltaTime;
            Debug.Log("针当前的位置：" + _ClickToInstantiate._NiddleObject.transform.position);
            
            //_ClickToInstantiate._NiddleObject.transform.position = Vector3.Lerp(_ClickToInstantiate._NiddleObject.transform.position, new Vector3(aimAnchorPos.x, aimAnchorPos.y, 0), Time.deltaTime * _AcupunctureSpeed);
        }
        else if(_IsAcupuncture == true && _AcupunctureTime >= _AcupunctureTotalTime)
        {
            _IsAcupuncture = false;
            Debug.Log("下针结束：" + _AcupunctureTime);

            //销毁针，重置下针参数，以便下次下针操作
            if(_DestroyCoroutine != null)
            {
                _DestroyCoroutine = null;
            }
            _DestroyCoroutine = StartCoroutine(WaitForTime(1f, () =>
            {
                _State = AcupunctureState.Nonesense;
                _AcupunctureTime = 0f;
                _OverTime = 0f;
                _MoveTime = 0f;
                DestroyNiddle();
                DestroyBezierObject();
            }));
            
        }
    }

    //销毁创建的针
    public void DestroyNiddle()
    {
        if(_ClickToInstantiate._NiddleObject != null)
        {
            Destroy(_ClickToInstantiate._NiddleObject);
            _ClickToInstantiate._IsSpawn = false;
            _ClickToInstantiate._NiddleObject = null;
            _State = AcupunctureState.Nonesense;
        }
    }

    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
