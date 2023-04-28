using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierMove : MonoBehaviour
{
    //三次Bezier控制点
    public Transform[] _ControlPoints;
    //Bezier插值个数
    public int _SegmentNum = 50;

    //线性渲染
    private LineRenderer _LineRenderer;
    private int _LayerOrder = 0;

    public Transform _Path;//弧线路径
    public float _WaitSpeed = 0.1f;//运动速度
    //private float _CurrentDis;//当前距离
    public int _CurrentPosNum;//当前在第几个点
    private Vector3 _LastPos;//上一次位置
    private bool _IsMovingRight;//true->right,false->left
    /*IEnumerator _CoroutineRight;
    IEnumerator _CoroutineLeft;*/
    float _TotalTime = 0f;
    private bool _IsFirstBuild = true;//是否是第一次创建


    public GameObject _MoveObject;//移动的物体
    public bool _TimeToMove = false;//判断是否可以移动

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

    /*//指针右移
    IEnumerator PointerMoveRight()
    {
        while(_CurrentPosNum < _SegmentNum-2)
        {
            yield return new WaitForSeconds(_WaitSpeed);
            _CurrentPosNum++;
        }
        _IsMovingRight = false;
    }

    //指针左移
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

    //销毁自己
    public void DestroySelf()
    {
        Destroy(this);
    }
}

