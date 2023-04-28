using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game03Help : MonoBehaviour
{
    public Button _Help;//帮助按钮
    public Button _Continue;//继续按钮
    public GameObject _HelpPanel;

    public GameObject _ClickTip;//点击提示
    public GameObject _AnchorNameForHelp;//穴位名
    //TipForClick _TipForClick;
    TipForAcu _TipForAcu;

    //第一组穴位名
    public GameObject _Anchor1;
    ClickToAcupuncture _ClickToAcupuncture;

    //public bool _IsFirstBegin;//是否是第一次开始

    void Start()
    {
        _Help.onClick.AddListener(OnHelpBtnClick);

        _Continue.onClick.AddListener(OnClickHelpBeginButton);

        //_TipForClick = _ClickTip.GetComponent<TipForClick>();

        _TipForAcu = _ClickTip.GetComponent<TipForAcu>();

        //_IsFirstBegin = true;

        _ClickToAcupuncture = _Anchor1.GetComponent<ClickToAcupuncture>();
    }

    void OnHelpBtnClick()
    {
        if (_HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
            _Continue.GetComponentInChildren<Text>().text = "继续";
        }
    }

    //HelpPanel的点击事件，设置HelpPanel为false
    void OnClickHelpBeginButton()
    {
        _HelpPanel.SetActive(false);
        
        /*//如果是第一次点击的话就显示穴位提示和倒计时。
        if(_IsFirstBegin == true)
        {

            //显示穴位名字与倒计时。
            _ClickToAcupuncture.enabled = true;

            _ClickTip.SetActive(true);

            _AnchorNameForHelp.SetActive(true);

            _TipForAcu._Coroutine = StartCoroutine(_TipForAcu.CountDownCoroutine());

            _IsFirstBegin = false;
        }*/
    }

}
