using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game00SettingPanelController : MonoBehaviour
{
    public Button _SettingClose;//������Ϸ��ť
    public Button _SettingExit;//�˳���ť
    public GameObject _SettingsPanel;//�������

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
