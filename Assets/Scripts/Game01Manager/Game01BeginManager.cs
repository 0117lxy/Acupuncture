using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game01
{
    public static int _NowLevel;
}

public class Game01BeginManager : MonoBehaviour
{
    public GameObject _HelpPanel;
    public Button _HelpPanelBeginButton;//帮助界面的开始游戏按钮，之后会变成继续游戏按钮
    public bool _IsGameFirstBegin = true;

    public GameObject _ClickTip;
    public GameObject[] _AnchorNameHelp;
    public GameObject _RewardPanel;//奖励面板
    
    void Start()
    {

        //游戏第一次开始，设置帮助面板可见，提示不可见
        if (_HelpPanel.activeSelf == false && _IsGameFirstBegin == true)
        {
            //设置当前展示的穴位组
            Game01._NowLevel = 0;

            _HelpPanel.SetActive(true);

            _AnchorNameHelp[Game01._NowLevel].SetActive(false);
        }

        //点击帮助面板
        _HelpPanelBeginButton.onClick.AddListener(delegate ()
        {
            //如果是第一次开始游戏，倒计时和提示设置可见
            if(_IsGameFirstBegin == true)
            {
                _AnchorNameHelp[Game01._NowLevel].SetActive(true);

                _ClickTip.GetComponent<TipForClick>()._Coroutine = StartCoroutine(_ClickTip.GetComponent<TipForClick>().CountDownCoroutine());
            }

            _IsGameFirstBegin = false;
            _HelpPanel.SetActive(false);
            
        });

    }
    
}
