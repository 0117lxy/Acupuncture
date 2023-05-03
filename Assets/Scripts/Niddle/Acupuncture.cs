using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Acupuncture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
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

    //��ʾ��Ϣ��ر���
    /*public int _TipNum;
    public bool[] _IsTip;*/

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

    void Start()
    {
        _ClickToInstantiate = GameObject.Find("NiddleType").GetComponent<ClickToInstantiate>();

        _Canvas = GameObject.Find("Canvas");

        _State = AcupunctureState.Nonesense;

        /*_TipNum = 4;
        _TipNum++;//������һ�����һλ�����ڰ����е���ʾ��Ϣ������
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

    //���ָ��������ȷ��button�ϣ���¼һ���������ת��λ�ã���������Ƴ�button����Ĳ����Ļָ�
    //���ý���button��boolֵΪtrue������AimAnchor()�����Ƿ������Ķ�׼�Լ�������������ж�
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

    //���ָ���Ƴ�button�ͻָ���ĳ�ʼ��ת��������ʱ��Ҳ����
    public void OnPointerExit(PointerEventData eventData)
    {
        AimAnchorExit(_EnterAnchor);
        _OverTime = 0f;
        //_EnterAnchor = false;
    }

    //�������ʱ����ʼ������������ʱ�䣬��¼�����boolֵΪtrue�������������룬��¼��ʼλ��
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

    //������ɿ���ʱ��������������ʱ�䣬���õ����boolֵΪfalse���������λ�ã����ٱ������ƶ�����
    public void OnPointerUp(PointerEventData eventData)
    {
        _MoveTime = 0f;
        _IsButtonDown = false;
        //_ClickToInstantiate._NiddleObject.transform.position = _InitialNiddlePos;
        DestroyNiddle();
        DestroyBezierObject();

    }

    //_EnterAnchorΪtrue��ʱ��_OverTime��֡����
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

    //��׼Ѩλ�ĺ���
    void AimAnchor(bool _EnterAnchor)
    {
        if(_EnterAnchor == true && _AimAnchor.tag =="Anchor" && _ClickToInstantiate._NiddleObject != null)
        {
            //��Ŀ������֮�����
            Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position); 
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            Debug.DrawLine(niddlePos, aimAnchorPos, Color.red);

            //��׼
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

            //�����ʱ�������y����������
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

    //�˳���׼�����������תΪ��ʼ��ת
    void AimAnchorExit(bool _EnterAnchor)
    {
        //�ж�_EnterAnchorΪtrue�����������п��������ܷ��ڱ��UI�ϣ�������жϿ��ܻ����
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
    
    //���ٱ������ƶ����壬�����Ѿ����������������boolֵΪfalse
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

    //��ָ���׼֮��Ϳ��Ը��ݱ������ƶ������ָ���λ�ý������룬����ָ���λ���ж������Ƿ���ȷ
    void ToAcupuncture()
    {
        if(_IsAcupuncture == true && _AcupunctureTime < _AcupunctureTotalTime)
        {
            _AcupunctureTime += Time.deltaTime;
            Debug.Log("�������룺" + _AcupunctureTime);

            //Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position);
            Vector3 aimAnchorPos = _AimAnchor.transform.position;

            Debug.Log("Ѩλλ�ã�" + aimAnchorPos);
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            Debug.Log("��λ�ã�" + niddlePos);
            Vector3 dir = aimAnchorPos - niddlePos;
            Debug.Log("����" + dir);
            Debug.DrawLine(niddlePos, aimAnchorPos);
            _ClickToInstantiate._NiddleObject.transform.position += dir * _AcupunctureSpeed * Time.deltaTime;
            Debug.Log("�뵱ǰ��λ�ã�" + _ClickToInstantiate._NiddleObject.transform.position);
            
            //_ClickToInstantiate._NiddleObject.transform.position = Vector3.Lerp(_ClickToInstantiate._NiddleObject.transform.position, new Vector3(aimAnchorPos.x, aimAnchorPos.y, 0), Time.deltaTime * _AcupunctureSpeed);
        }
        else if(_IsAcupuncture == true && _AcupunctureTime >= _AcupunctureTotalTime)
        {
            _IsAcupuncture = false;
            Debug.Log("���������" + _AcupunctureTime);

            //�����룬��������������Ա��´��������
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

    //���ٴ�������
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
