using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game02WinOrLose : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _WinPanel;//胜利界面
    public Button _WinBackButton;//胜利界面的返回按钮
    public GameObject _LosePanel;//失败界面
    public Button _LoseBackButton;//失败界面的返回按钮
    public GameObject _LifeNumberObj;//生命值，用于获取nowHeartNumber
    public GameObject _ClickTip;//点击提示组件，用于获取_ClickAll
    private string _BackScene;

    public GameObject _RewardPanel;//奖励面板
    public Button _RewardPanelButton;

    // Start is called before the first frame update
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
        if (_ClickTip.GetComponent<TipForAcu>()._ClickAll == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0)
        {
            //_WinPanel.SetActive(true);
            _RewardPanel.SetActive(true);
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
