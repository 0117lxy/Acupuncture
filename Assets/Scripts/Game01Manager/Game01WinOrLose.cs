using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game01WinOrLose : MonoBehaviour
{
    public GameObject _WinPanel;//胜利界面
    public Button _WinBackButton;//胜利界面的返回按钮
    public GameObject _LosePanel;//失败界面
    public Button _LoseBackButton;//失败界面的返回按钮
    public GameObject _LifeNumberObj;//生命值，用于获取nowHeartNumber
    public GameObject _ClickTip;//点击提示组件，用于获取_ClickAll
    public string _BackScene;

    public GameObject _NextGroupPanel;//奖励面板
    public Button __NextGroupPanelButton;//奖励界面继续游戏的按钮

    public GameObject[] _AnchorNameHelp;

    public GameObject[] _Anchors;//穴位组

    public GameObject _TipObject;//提示物体

    public GameObject _RewardPanel;//奖励面板
    public Button _RewardPanelButton;

    void Start()
    {
        _BackScene = "Game00";

        _RewardPanelButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });

        _WinBackButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });

        _LoseBackButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });

        __NextGroupPanelButton.onClick.AddListener(delegate ()
        {
            _RewardPanel.SetActive(false);
            _Anchors[Game01._NowLevel].SetActive(true);
            _TipObject.GetComponent<Text>().enabled = true;
            _AnchorNameHelp[Game01._NowLevel].SetActive(true);
            _ClickTip.GetComponent<TipForClick>()._CountDownTime = 3;
            _ClickTip.GetComponent<TipForClick>()._Coroutine = StartCoroutine(_ClickTip.GetComponent<TipForClick>().CountDownCoroutine());          
        });
    }

    // Update is called once per frame
    void Update()
    {
        ToReward();
        ToWin();
        ToLose();
    }

    //胜利判断
    void ToWin()
    {
        if(_ClickTip.GetComponent<TipForClick>()._ClickAll[Game01._NowLevel] == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0 && Game01._NowLevel == 2)
        {
            //_WinPanel.SetActive(true);
            _RewardPanel.SetActive(true);
        }
    }

    //失败判断
    void ToLose()
    {
        if(_LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber == 0)
        {
            _LosePanel.SetActive(true);
        }
    }

    void ToReward()
    {
        if(_ClickTip.GetComponent<TipForClick>()._ClickAll[Game01._NowLevel] == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0 && Game01._NowLevel < 2)
        {
            _Anchors[Game01._NowLevel].SetActive(false);
            Game01._NowLevel++;
            _TipObject.GetComponent<Text>().enabled = false;
            _ClickTip.GetComponent<TipForClick>().clickNum = 0;
            _RewardPanel.SetActive(true);
        }
    }
}
