using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game01BeginManager : MonoBehaviour
{
    public GameObject _HelpPanel;
    public Button _HelpPanelBeginButton;//帮助界面的开始游戏按钮，之后会变成继续游戏按钮
    public bool _IsGameFirstBegin = true;
    GameObject _CountDown;
    GameObject _ClickTip;
    // Start is called before the first frame update
    void Start()
    {
        _CountDown = GameObject.Find("AnchorNameHelp");
        _ClickTip = GameObject.Find("ClickTip");

        if (_HelpPanel.activeSelf == false && _IsGameFirstBegin == true)
        {
            _HelpPanel.SetActive(true);
            _CountDown.SetActive(false);
        }
        _HelpPanelBeginButton.onClick.AddListener(delegate ()
        {
            _IsGameFirstBegin = false;
            _HelpPanel.SetActive(false);
            _CountDown.SetActive(true);
            _ClickTip.GetComponent<TipForClick>()._Coroutine = StartCoroutine(_ClickTip.GetComponent<TipForClick>().CountDownCoroutine());
        });
    }
    
}
