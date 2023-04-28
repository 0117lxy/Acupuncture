using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game02BeginManager : MonoBehaviour
{
    public GameObject _HelpPanel;
    public Button _HelpPanelBeginButton;//帮助界面的开始游戏按钮，之后会变成继续游戏按钮
    public bool _IsGameFirstBegin = true;

    //用于新手指引
    public GameObject _TipInfo;
    TipInfoController _TipInfoController;

    /*GameObject _CountDown;
    GameObject _ClickTip;*/

    //用于开始第二关
    public GameObject _Anchor1;//第一组穴位
    ClickAnchor _ClickAnchor1;
    ClickToAcupuncture _ClickToAcupuncture;
    public GameObject _AnchorNameHelp;//穴位名
    public GameObject _ClickTip;//提示需要点击的穴位名的物体
    //TipForClick _TipForClick;
    TipForAcu _TipForAcu;

    //开始练习游戏的提示
    public GameObject _BeginTest;
    private bool _IsShowBeginTest = false;
    Coroutine _BeginCoroutine;

    //关卡2开始时设置_HelpPanel的Active为True
    private void Start()
    {
        //_TipInfo = GameObject.Find("TipInfo");

        _TipInfoController = _TipInfo.GetComponent<TipInfoController>();

        if (_IsGameFirstBegin == true && _HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
        }

        _HelpPanelBeginButton.onClick.AddListener(OnClickHelpBeginButton);

        //_ClickAnchor1 = _Anchor1.GetComponent<ClickAnchor>();
        _ClickToAcupuncture = _Anchor1.GetComponent<ClickToAcupuncture>();

        //_TipForClick = _ClickTip.GetComponent<TipForClick>();
        
        _TipForAcu = _ClickTip.GetComponent<TipForAcu>();   
    }

    private void Update()
    {

        //只显示一次开始练习的提示
        if (_TipInfoController._IsShowAllTips == true && _BeginTest.activeSelf == false && _IsShowBeginTest == false)
        {
            if(_BeginCoroutine == null)
            {
                _BeginCoroutine = StartCoroutine(WaitForTime(2f, () =>
                {
                    _BeginTest.SetActive(true);
                    _IsShowBeginTest = true;
                    Invoke("HideTipAndShowDetails", 1f);

                }));
            }
            
        }

    }

    //HelpPanel的点击事件，设置HelpPanel为false
    void OnClickHelpBeginButton()
    {
        _IsGameFirstBegin = false;
        _HelpPanel.SetActive(false);

        _TipInfoController._Tips[0].SetActive(true);
    }

    //显示穴位、穴位名字、提示等信息
    void ShowGameDetails()
    {
        if (_TipInfoController._IsShowAllTips == true)
        {

            _ClickToAcupuncture.enabled = true;

            _ClickTip.SetActive(true);

            //显示穴位
            for (int i = 0; i < _ClickToAcupuncture.anchor1Number; i++)
            {
                if (_Anchor1.transform.GetChild(i).gameObject.activeSelf == false)
                {
                    _Anchor1.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            _AnchorNameHelp.SetActive(true);

        }
    }

    //销毁开始练习游戏的提示，展示游戏关卡物体，开始练习游戏
    void HideTipAndShowDetails()
    {
        _BeginTest.SetActive(false);

        ShowGameDetails();

        _TipForAcu._Coroutine = StartCoroutine(_TipForAcu.CountDownCoroutine());
    }

    //等待duration秒的方法
    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
