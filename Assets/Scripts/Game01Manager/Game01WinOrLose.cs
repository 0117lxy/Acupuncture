using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game01WinOrLose : MonoBehaviour
{
    public GameObject _WinPanel;//ʤ������
    public Button _WinBackButton;//ʤ������ķ��ذ�ť
    public GameObject _LosePanel;//ʧ�ܽ���
    public Button _LoseBackButton;//ʧ�ܽ���ķ��ذ�ť
    public GameObject _LifeNumberObj;//����ֵ�����ڻ�ȡnowHeartNumber
    public GameObject _ClickTip;//�����ʾ��������ڻ�ȡ_ClickAll
    public string _BackScene;

    // Start is called before the first frame update
    void Start()
    {
        _BackScene = "Game00";

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

    //ʤ���ж�
    void ToWin()
    {
        if(_ClickTip.GetComponent<TipForClick>()._ClickAll == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0)
        {
            _WinPanel.SetActive(true);
        }
    }

    //ʧ���ж�
    void ToLose()
    {
        if(_LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber == 0)
        {
            _LosePanel.SetActive(true);
        }
    }
}