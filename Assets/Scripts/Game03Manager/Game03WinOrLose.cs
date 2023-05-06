using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game03WinOrLose : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _WinPanel;//胜利界面
    public Button _WinBackButton;//胜利界面的返回按钮
    public GameObject _LosePanel;//失败界面
    public Button _LoseBackButton;//失败界面的返回按钮
    public GameObject _LifeNumberObj;//生命值，用于获取nowHeartNumber
    public GameObject _ClickTip;//点击提示组件，用于获取_ClickAll
    private string _WinBackScene;//胜利返回界面
    private string _LoseBackScene;//失败返回界面


    public GameObject _RewardPanel;//奖励面板
    public Button _RewardPanelButton;
    private string _RewardBackScene;//失败返回界面

    // Start is called before the first frame update
    void Start()
    {

        _RewardBackScene = "Game04";
        _RewardPanelButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _RewardBackScene;
            SceneManager.LoadScene("Loading");
        });

        _WinBackScene = "Game04";
        _WinBackButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _WinBackScene;
            SceneManager.LoadScene("Loading");
        });

        _LoseBackScene = "Game00";
        _LoseBackButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _LoseBackScene;
            SceneManager.LoadScene("Loading");
        });

    }

    // Update is called once per frame
    void Update()
    {
        ToWin();
        ToLose();
    }

    //胜利判断
    void ToWin()
    {
        if (_ClickTip.GetComponent<TipForAcuDescirption>()._ClickAll == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0)
        {
            _WinPanel.SetActive(true);
            //_RewardPanel.SetActive(true);
            //Reward._IsHaveReward[3] = true;
        }
    }

    //失败判断
    void ToLose()
    {
        if (_LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber == 0)
        {
            _LosePanel.SetActive(true);
        }
    }
}
