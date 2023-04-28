using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game01Setting : MonoBehaviour
{
    public Button _Setting;//设置按钮
    public GameObject _SettingPanel;//设置面板

    // Start is called before the first frame update
    void Start()
    {
        _Setting.onClick.AddListener(delegate ()
        {
            OnClickSetting();
        });
    }

    void OnClickSetting()
    {
        if(_SettingPanel.activeSelf == false)
        {
            _SettingPanel.SetActive(true);
        }
    }
}
