using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClickToAcupuncture : MonoBehaviour
{
    public Button[] anchors1;//穴位
    public string[] _Anchor1Names;//穴位名

    public string[] _Anchor1Description;//穴位描述

    public int anchor1Number;
    private bool isClick;

    List<ButtonGroup> _ButtonGroups;
    Button _ButtonBack;

    //显示点击穴位的名字
    public Canvas canvas;
    UIManagerController _UIManagerController;

    //是否下针正确相关变量
    Acupuncture _Acupuncture;
    Acupuncture2 _Acupuncture2;
    GameObject _BezierObject;
    BezierMove _BezierMove;
    public int _LeftBezierPos;
    public int _RightBezierPos;
    int _NowBezierPos;
    bool _IsAcupunctureRight;
    int _Index;


    class ButtonGroup
    {
        public Button _Anchor;
        uint index;
        public uint Index
        {
            set
            {
                index = value;
            }
        }
        public void Register(Action<uint, GameObject> clickEvent)
        {
            _Anchor.onClick.AddListener(() =>
            {
                clickEvent(index, _Anchor.gameObject);
            });
        }
    }

    LifeNumberChange lifeNumberChange;//生命值控制

    //TipForClick tipForClick;//点击穴位的提示

    TipForAcu tipForAcu;//点击穴位的提示（下针）

    private void Awake()
    {
        _ButtonGroups = new List<ButtonGroup>();

        _IsAcupunctureRight = false;
    }
    void Start()
    {
        isClick = false;

        for (int i = 0; i < anchors1.Length; i++)
        {
            _ButtonGroups.Add(new ButtonGroup());
            _ButtonGroups[i]._Anchor = anchors1[i];
        }

        InitButtons();

        lifeNumberChange = GameObject.Find("LifeNumber").GetComponent<LifeNumberChange>();

        //tipForClick = GameObject.Find("ClickTip").GetComponent<TipForClick>();

        tipForAcu = GameObject.Find("ClickTip").GetComponent<TipForAcu>();

        _UIManagerController = canvas.GetComponent<UIManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetMouseButtonUp(0) && isClick == false)
        {
            //isClick = false;
            if (lifeNumberChange.theHeartNumber != 0)
            {
                lifeNumberChange.theHeartNumber--;
            }
        }*/

        //判断是否点击到穴位
        /*if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                isClick = false;
            }
            else
            {
                isClick = true;
            }
        }*/

    }

    private void LateUpdate()
    {
        IsAcupunctureRight();

        //IsAcupuncture2Right();
    }

    /*void OnAnchorClick(ButtonGroup buttonGroup)
    {
        Debug.Log("Click!!!");
        isClick = true;
        buttonGroup._Anchor.gameObject.SetActive(false);
    }*/

    void InitButtons()
    {
        for (int i = 0; i < _ButtonGroups.Count; i++)
        {
            var button = _ButtonGroups[i];
            button.Index = (uint)i;
            button.Register(ButtonClick);
        }
    }
    void ButtonClick(uint index, GameObject anchor)
    {

        Debug.Log("Click!!!");

        _Acupuncture = anchor.GetComponent<Acupuncture>();
        
        if (_Acupuncture._BezierObject != null)
        {
            _BezierObject = _Acupuncture._BezierObject;
            _BezierMove = _BezierObject.GetComponent<BezierMove>();
        }
  

        isClick = true;


        if (index != tipForAcu.clickQueue[tipForAcu.clickNum])
        {
            if (lifeNumberChange.theHeartNumber != 0)
            {
                lifeNumberChange.theHeartNumber--;

                DestroyAcupuncture();

                //DestroyAcupuncture2();
            }
        }
        else
        {

            _IsAcupunctureRight = true;
            _Index = (int)index;
                   

            //Debug.Log(""+_UIManagerController.name);
            //_ButtonGroups[(int)index]._Anchor.gameObject.SetActive(false);
            //tipForClick.isClicked[tipForClick.clickQueue[tipForClick.clickNum]] = true;
        }

    }
    
    //判断下针是否正确的函数
    void IsAcupunctureRight()
    {
        if(_IsAcupunctureRight == true && _Acupuncture != null)
        {
            if(_Acupuncture._State == Acupuncture.AcupunctureState.Niddling)
            {
                _NowBezierPos = _BezierMove._CurrentPosNum;

                if(!(_NowBezierPos >= _LeftBezierPos && _NowBezierPos <= _RightBezierPos))
                {
                    if (lifeNumberChange.theHeartNumber != 0)
                    {
                        lifeNumberChange.theHeartNumber--;
                    }

                    DestroyAcupuncture();
                }
                else
                {
                    StartCoroutine(WaitForTime(1f, () =>
                    {
                        _ButtonGroups[_Index]._Anchor.gameObject.SetActive(false);
                        tipForAcu.isClicked[tipForAcu.clickQueue[tipForAcu.clickNum]] = true;
                        RectTransform rectTransform = _ButtonGroups[_Index]._Anchor.GetComponent<RectTransform>();
                        _UIManagerController.ShowUpAnchorName(rectTransform.position, _Anchor1Names[_Index]);
                        DestroyAcupuncture();
                    }));
                }
                _IsAcupunctureRight = false;
                
            }
        }
    }

    void IsAcupuncture2Right()
    {
        if (_IsAcupunctureRight == true && _Acupuncture2 != null)
        {
            if (_Acupuncture2._State == Acupuncture2.AcupunctureState.Niddling)
            {
                _NowBezierPos = _BezierMove._CurrentPosNum;

                if (!(_NowBezierPos >= _LeftBezierPos && _NowBezierPos <= _RightBezierPos))
                {
                    if (lifeNumberChange.theHeartNumber != 0)
                    {
                        lifeNumberChange.theHeartNumber--;
                    }

                    //DestroyAcupuncture();
                }
                else
                {
                    StartCoroutine(WaitForTime(1f, () =>
                    {
                        _ButtonGroups[_Index]._Anchor.gameObject.SetActive(false);
                        tipForAcu.isClicked[tipForAcu.clickQueue[tipForAcu.clickNum]] = true;
                        RectTransform rectTransform = _ButtonGroups[_Index]._Anchor.GetComponent<RectTransform>();
                        _UIManagerController.ShowUpAnchorName(rectTransform.position, _Anchor1Names[_Index]);
                    }));
                }
                _IsAcupunctureRight = false;
            }
        }
    }

    void DestroyAcupuncture()
    {
        _Acupuncture._IsAcupuncture = false;
        _Acupuncture.DestroyNiddle();
        _Acupuncture.DestroyBezierObject();
        _Acupuncture = null;
    }

    void DestroyAcupuncture2()
    {
        _Acupuncture2.DestroyBezierObject();
        _Acupuncture2.DestroyNiddle();
        _Acupuncture2 = null;
    }

    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
