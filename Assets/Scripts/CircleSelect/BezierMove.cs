using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierMove : MonoBehaviour
{
    //����Bezier���Ƶ�
    public Transform[] _ControlPoints;
    //Bezier��ֵ����
    public int _SegmentNum = 50;

    //������Ⱦ
    private LineRenderer _LineRenderer;
    private int _LayerOrder = 0;

    public Transform _Path;//����·��
    public float _WaitSpeed = 0.1f;//�˶��ٶ�
    //private float _CurrentDis;//��ǰ����
    public int _CurrentPosNum;//��ǰ�ڵڼ�����
    private Vector3 _LastPos;//��һ��λ��
    private bool _IsMovingRight;//true->right,false->left
    /*IEnumerator _CoroutineRight;
    IEnumerator _CoroutineLeft;*/
    float _TotalTime = 0f;
    private bool _IsFirstBuild = true;//�Ƿ��ǵ�һ�δ���


    public GameObject _MoveObject;//�ƶ�������
    public bool _TimeToMove = false;//�ж��Ƿ�����ƶ�

    void Start()
    {
        if(!_LineRenderer)
        {
            _LineRenderer = GetComponent<LineRenderer>();
        }
        _LineRenderer.sortingLayerID = _LayerOrder;

        _CurrentPosNum = 1;
        _LastPos = _MoveObject.transform.position;
        _IsMovingRight = true;

        /*_CoroutineRight = PointerMoveRight();
        _CoroutineLeft = PointerMoveLeft();*/
    }
    void Update()
    {
        Draw3BezierCurve();
        
        Vector3 pos = _Path.GetComponent<LineRenderer>().GetPosition(_CurrentPosNum); 
        _MoveObject.transform.position = pos;
        //Vector3 deltaPos = _MoveObject.transform.position - _LastPos;
        //_MoveObject.transform.rotation = Quaternion.LookRotation(deltaPos);
        if(!_IsFirstBuild)
        {
            _MoveObject.transform.LookAt(_LastPos);
        }
        _IsFirstBuild = false;
        _LastPos = _MoveObject.transform.position;

        StartMove(_TimeToMove);
    }

    Vector3[] points;
    void Draw3BezierCurve()
    {
        points = BezierUtils.GetThreePowerBeizerList(_ControlPoints[0].position, _ControlPoints[1].position, _ControlPoints[2].position, _ControlPoints[3].position, _SegmentNum);
        _LineRenderer.positionCount = _SegmentNum;
        _LineRenderer.SetPositions(points);
    }

    /*//ָ������
    IEnumerator PointerMoveRight()
    {
        while(_CurrentPosNum < _SegmentNum-2)
        {
            yield return new WaitForSeconds(_WaitSpeed);
            _CurrentPosNum++;
        }
        _IsMovingRight = false;
    }

    //ָ������
    IEnumerator PointerMoveLeft()
    {
        while (_CurrentPosNum > 1)
        {
            yield return new WaitForSeconds(_WaitSpeed);
            _CurrentPosNum--;
        }
        _IsMovingRight = true;
    }*/

    void StartMove(bool _TimeToMove)
    {
        if(_TimeToMove == false)
        {
            return;
        }
        _TotalTime += Time.deltaTime;
        if (_IsMovingRight == true)
        {
            if (_CurrentPosNum < _SegmentNum - 1)
            {
                if (_TotalTime > _WaitSpeed)
                {
                    _TotalTime = 0f;
                    _CurrentPosNum++;
                }
            }
            else
            {
                _IsMovingRight = false;
            }
        }
        else if (_IsMovingRight == false)
        {
            if (_CurrentPosNum > 0)
            {
                if (_TotalTime > _WaitSpeed)
                {
                    _TotalTime = 0f;
                    _CurrentPosNum--;
                }
            }
            else
            {
                _IsMovingRight = true;
            }
        }
    }

    //�����Լ�
    public void DestroySelf()
    {
        Destroy(this);
    }
}

