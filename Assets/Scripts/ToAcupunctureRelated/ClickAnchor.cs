using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClickAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] anchors1;
    public string[] _Anchor1Names;

    public int anchor1Number;
    private bool isClick;

    List<ButtonGroup> _ButtonGroups;
    Button _ButtonBack;

    //显示点击穴位的名字
    public Canvas canvas;
    UIManagerController _UIManagerController;
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
        public void Register(Action<uint> clickEvent)
        {
            _Anchor.onClick.AddListener(() =>
            {
                clickEvent(index);
            });
        }
    }
    

    LifeNumberChange lifeNumberChange;
    TipForClick tipForClick;
    private void Awake()
    {
        //anchors1 = new Button[anchor1Number];
        _ButtonGroups = new List<ButtonGroup>();
    }
    void Start()
    {
        isClick = false;
        /*foreach(var anchor in anchors1)
        {
            anchor.onClick.AddListener(delegate ()
            {
                OnAnchorClick(anchor);
            });
            //Debug.Log("Ok!!!");
        }*/
        for(int i=0; i < anchors1.Length; i++)
        {
            _ButtonGroups.Add(new ButtonGroup());
            _ButtonGroups[i]._Anchor = anchors1[i];
        }
        InitButtons();

        lifeNumberChange = GameObject.Find("LifeNumber").GetComponent<LifeNumberChange>();
        tipForClick = GameObject.Find("ClickTip").GetComponent<TipForClick>();
        _UIManagerController = canvas.GetComponent<UIManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && isClick == false)
        {
            //isClick = false;
            if(lifeNumberChange.theHeartNumber!=0)
            {
                lifeNumberChange.theHeartNumber--;
            }
        }

        //判断是否点击到穴位
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                isClick = false;
            }
            else
            {
                isClick = true;
            }
        }
    }

    /*void OnAnchorClick(ButtonGroup buttonGroup)
    {
        Debug.Log("Click!!!");
        isClick = true;
        buttonGroup._Anchor.gameObject.SetActive(false);
    }*/

    void InitButtons()
    {
        for(int i=0; i<_ButtonGroups.Count; i++)
        {
            var button = _ButtonGroups[i];
            button.Index = (uint)i;
            button.Register(ButtonClick);
        }
    }
    void ButtonClick(uint index)
    {
        Debug.Log("Click!!!");
        isClick = true;
        if(index != tipForClick.clickQueue[tipForClick.clickNum])
        {
            if (lifeNumberChange.theHeartNumber != 0)
            {
                lifeNumberChange.theHeartNumber--;
            }
        }
        else
        {
            RectTransform rectTransform = _ButtonGroups[(int)index]._Anchor.GetComponent<RectTransform>();
            _UIManagerController.ShowUpAnchorName(rectTransform.position, _Anchor1Names[(int)index]);
            //Debug.Log(""+_UIManagerController.name);
            _ButtonGroups[(int)index]._Anchor.gameObject.SetActive(false);
            tipForClick.isClicked[tipForClick.clickQueue[tipForClick.clickNum]] = true;
        }
    }
    
}
