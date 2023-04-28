using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game00SettingPanelController : MonoBehaviour
{
    public Button _SettingClose;//返回游戏按钮
    public Button _SettingExit;//退出按钮
    public GameObject _SettingsPanel;//设置面板

    private void Start()
    {
        _SettingClose.onClick.AddListener(delegate ()
        {
            if (_SettingsPanel.activeSelf == true)
            {
                _SettingsPanel.SetActive(false);
            }
        });

        _SettingExit.onClick.AddListener(delegate ()
        {
            //BackToMainScene("Game00");
            Application.Quit();
        });
    }

    void BackToMainScene(string sceneName)
    {
        Globe._NextSceneName = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
