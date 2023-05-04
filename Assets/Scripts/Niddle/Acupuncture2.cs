using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Acupuncture2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private float _OverTime = 0f;//��ͣʱ��
    private bool _EnterAnchor = false;//�Ƿ���ͣ��Ѩλ��
    //private Button _AimAnchor;//��ͣ��Button
    private GameObject _AimAnchor;//��ͣ��Button
    public float _AimTime;//��׼ʱ��

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
        BringNiddle();

        switch (_State)
        {
            case AcupunctureState.Bring:
                break;
            case AcupunctureState.Focus:
                //AimAnchor();
                StartCoroutine(Aim());
                break;
            case AcupunctureState.FocusOver:
                BuildBezier();
                break;
            case AcupunctureState.Strengthen:
                NiddleStrenthen();
                break;
            case AcupunctureState.StrengthenOver:
                TimeToNiddle();
                break;
            case AcupunctureState.Niddling:
                StartCoroutine(Acu());
                StopAllCoroutines();
                break;
            case AcupunctureState.Nonesense:
                BringNiddle();
                break;
            default:
                break;
        }
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.CompareTag("Anchor") && _State == AcupunctureState.Bring)
        {
            _State = AcupunctureState.Focus;
            _AimAnchor = this.gameObject;
            _EnterAnchor = true;
            _InitialNiddleRotation = _ClickToInstantiate._NiddleRotation;
            _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;

            Debug.Log("_AimAnchor: " + _AimAnchor);
            Debug.Log("_State: " + _State);

        }    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AimAnchorExit(_EnterAnchor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_State == AcupunctureState.FocusOver && _IsBuildBezier == true && _ClickToInstantiate._IsSpawn == true)
        {
            _State = AcupunctureState.Strengthen;
            _IsButtonDown = true;
            _ClickToInstantiate._IsAcupuncture = true;
            _InitialNiddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            _MoveTime = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _MoveTime = 0f;
        _IsButtonDown = false;
        _State = AcupunctureState.Nonesense;
        DestroyNiddle();
        DestroyBezierObject();
    }

    //������
    void BringNiddle()
    {
        if (_ClickToInstantiate._IsSpawn == true && _State == AcupunctureState.Nonesense)
        {
            _State = AcupunctureState.Bring;
        }
    }

    //��׼
    void AimAnchor()
    {
        //��Ŀ������֮�����
        Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position);
        Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
        Debug.DrawLine(niddlePos, aimAnchorPos, Color.red);

        _ClickToInstantiate._NiddleObject.transform.rotation = Quaternion.Slerp(_ClickToInstantiate._NiddleObject.transform.rotation, Quaternion.Euler(_LastNiddleRotation), _RotateSpeed * Time.deltaTime);

        _State = AcupunctureState.FocusOver;
    }

    IEnumerator Aim()
    {
        float t = 0;
        while(t < _AimTime)
        {
            t += Time.deltaTime;
            _ClickToInstantiate._NiddleObject.transform.rotation = Quaternion.Slerp(_ClickToInstantiate._NiddleObject.transform.rotation, Quaternion.Euler(_LastNiddleRotation), _RotateSpeed * Time.deltaTime);
            yield return null;
        }

        _State = AcupunctureState.FocusOver;

        yield break;
    }

    //�˳���׼�����������תΪ��ʼ��ת
    void AimAnchorExit(bool _EnterAnchor)
    {
        //�ж�_EnterAnchorΪtrue�����������п��������ܷ��ڱ��UI�ϣ�������жϿ��ܻ����
        if (_EnterAnchor == true)
        {
            if (_ClickToInstantiate._NiddleObject != null)
            {
                _ClickToInstantiate._NiddleObject.transform.rotation = _InitialNiddleRotation;
            }
            _ClickToInstantiate._IsAcupuncture = false;
            this._EnterAnchor = false;
            _State = AcupunctureState.Bring;
            DestroyBezierObject();
        }
    }

    //����Bezier
    void BuildBezier()
    {
        if(_IsBuildBezier == false)
        {
            _BezierObject = Instantiate(_BezierPrefab, _BezierPos, _BezierRotation);

            _IsBuildBezier = true;
        }

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

    //����
    void NiddleStrenthen()
    {
        if(_MoveTime < 1.0f)
        {
            _MoveTime += Time.deltaTime;
            Vector3 moveDir = _ClickToInstantiate._NiddleObject.transform.up;
            _ClickToInstantiate._NiddleObject.transform.position += moveDir * _MoveSpeed * Time.deltaTime;

            if (_IsBuildBezier == true)
            {
                _BezierObject.GetComponent<BezierMove>()._TimeToMove = true;
            }
        }
        else
        {
            _State = AcupunctureState.StrengthenOver;
        }
    }

    //��������ı�־
    void TimeToNiddle()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _State = AcupunctureState.Niddling;

            if (_IsBuildBezier == true)
            {
                _BezierObject.GetComponent<BezierMove>()._TimeToMove = false;
            }
        }
    }

    //��ָ���׼֮��Ϳ��Ը��ݱ������ƶ������ָ���λ�ý������룬����ָ���λ���ж������Ƿ���ȷ
    void ToAcupuncture()
    {
        if (_IsAcupuncture == true && _AcupunctureTime < _AcupunctureTotalTime)
        {
            _AcupunctureTime += Time.deltaTime;
            //Debug.Log("�������룺" + _AcupunctureTime);

            //Vector3 aimAnchorPos = Camera.main.ScreenToWorldPoint(_AimAnchor.transform.position);
            Vector3 aimAnchorPos = _AimAnchor.transform.position;

            //Debug.Log("Ѩλλ�ã�" + aimAnchorPos);
            Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
            //Debug.Log("��λ�ã�" + niddlePos);
            Vector3 dir = aimAnchorPos - niddlePos;
            //Debug.Log("����" + dir);
            //Debug.DrawLine(niddlePos, aimAnchorPos, Color.red);
            _ClickToInstantiate._NiddleObject.transform.position += dir * _AcupunctureSpeed * Time.deltaTime;
            //Debug.Log("�뵱ǰ��λ�ã�" + _ClickToInstantiate._NiddleObject.transform.position);

            //_ClickToInstantiate._NiddleObject.transform.position = Vector3.Lerp(_ClickToInstantiate._NiddleObject.transform.position, new Vector3(aimAnchorPos.x, aimAnchorPos.y, 0), Time.deltaTime * _AcupunctureSpeed);
        }
        else if (_IsAcupuncture == true && _AcupunctureTime >= _AcupunctureTotalTime)
        {
            _IsAcupuncture = false;
            Debug.Log("���������" + _AcupunctureTime);

            //�����룬��������������Ա��´��������
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

    IEnumerator Acu()
    {
        Vector3 aimAnchorPos = _AimAnchor.transform.position;
        Vector3 niddlePos = _ClickToInstantiate._NiddleObject.transform.position;
        Vector3 dir = aimAnchorPos - niddlePos;
        float t = 0f;
        while(t < _AcupunctureTotalTime)
        {
            t += Time.deltaTime;
            float x = Mathf.SmoothStep(niddlePos.x, aimAnchorPos.x, t / _AcupunctureTotalTime);
            float y = Mathf.SmoothStep(niddlePos.y, aimAnchorPos.y, t / _AcupunctureTotalTime);
            float z = Mathf.SmoothStep(niddlePos.z, aimAnchorPos.z, t / _AcupunctureTotalTime);
            Vector3 pos = new Vector3(x, y, z);
            _ClickToInstantiate._NiddleObject.transform.position = pos;
            yield return null;
        }

        _IsAcupuncture = false;
        //Debug.Log("���������" + _AcupunctureTime);

        _DestroyCoroutine = StartCoroutine(WaitForTime(1f, () =>
        {
            Debug.Log("�����������");
            _AcupunctureTime = 0f;
            _OverTime = 0f;
            _MoveTime = 0f;
            DestroyNiddle();
            DestroyBezierObject();
            _State = AcupunctureState.Nonesense;
        }));

        yield break;
    }

    //���ٴ�������
    public void DestroyNiddle()
    {
        if (_ClickToInstantiate._NiddleObject != null)
        {
            Debug.Log("DestroyNiddle called for: " + _ClickToInstantiate._NiddleObject.name);

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