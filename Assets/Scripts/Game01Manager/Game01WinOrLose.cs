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

    public GameObject _RewardPanel;//�������
    public Button _RewardContinueButton;//�������������Ϸ�İ�ť

    public GameObject[] _AnchorNameHelp;

    public GameObject[] _Anchors;//Ѩλ��

    public GameObject _TipObject;//��ʾ����

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

        _RewardContinueButton.onClick.AddListener(delegate ()
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

    //ʤ���ж�
    void ToWin()
    {
        if(_ClickTip.GetComponent<TipForClick>()._ClickAll[Game01._NowLevel] == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0 && Game01._NowLevel == 2)
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
