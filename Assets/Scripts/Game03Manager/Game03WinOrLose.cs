using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game03WinOrLose : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _WinPanel;//ʤ������
    public Button _WinBackButton;//ʤ������ķ��ذ�ť
    public GameObject _LosePanel;//ʧ�ܽ���
    public Button _LoseBackButton;//ʧ�ܽ���ķ��ذ�ť
    public GameObject _LifeNumberObj;//����ֵ�����ڻ�ȡnowHeartNumber
    public GameObject _ClickTip;//�����ʾ��������ڻ�ȡ_ClickAll
    private string _WinBackScene;//ʤ�����ؽ���
    private string _LoseBackScene;//ʧ�ܷ��ؽ���


    public GameObject _RewardPanel;//�������
    public Button _RewardPanelButton;
    private string _RewardBackScene;//ʧ�ܷ��ؽ���

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

    //ʤ���ж�
    void ToWin()
    {
        if (_ClickTip.GetComponent<TipForAcuDescirption>()._ClickAll == true && _LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber > 0)
        {
            _WinPanel.SetActive(true);
            //_RewardPanel.SetActive(true);
            //Reward._IsHaveReward[3] = true;
        }
    }

    //ʧ���ж�
    void ToLose()
    {
        if (_LifeNumberObj.GetComponent<LifeNumberChange>().nowHeartNumber == 0)
        {
            _LosePanel.SetActive(true);
        }
    }
}
