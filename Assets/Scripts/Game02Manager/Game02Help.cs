using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game02Help : MonoBehaviour
{
    public Button _Help;//������ť
    public Button _Continue;//������ť
    public GameObject _HelpPanel;

    void Start()
    {
        _Help.onClick.AddListener(OnHelpBtnClick);
    }

    void OnHelpBtnClick()
    {
        if (_HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
            _Continue.GetComponentInChildren<Text>().text = "����";
        }
    }
}
