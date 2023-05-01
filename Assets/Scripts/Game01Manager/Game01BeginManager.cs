using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game01
{
    public static int _NowLevel;
}

public class Game01BeginManager : MonoBehaviour
{
    public GameObject _HelpPanel;
    public Button _HelpPanelBeginButton;//��������Ŀ�ʼ��Ϸ��ť��֮����ɼ�����Ϸ��ť
    public bool _IsGameFirstBegin = true;

    public GameObject _ClickTip;
    public GameObject[] _AnchorNameHelp;
    public GameObject _RewardPanel;//�������
    
    void Start()
    {

        //��Ϸ��һ�ο�ʼ�����ð������ɼ�����ʾ���ɼ�
        if (_HelpPanel.activeSelf == false && _IsGameFirstBegin == true)
        {
            //���õ�ǰչʾ��Ѩλ��
            Game01._NowLevel = 0;

            _HelpPanel.SetActive(true);

            _AnchorNameHelp[Game01._NowLevel].SetActive(false);
        }

        //����������
        _HelpPanelBeginButton.onClick.AddListener(delegate ()
        {
            //����ǵ�һ�ο�ʼ��Ϸ������ʱ����ʾ���ÿɼ�
            if(_IsGameFirstBegin == true)
            {
                _AnchorNameHelp[Game01._NowLevel].SetActive(true);

                _ClickTip.GetComponent<TipForClick>()._Coroutine = StartCoroutine(_ClickTip.GetComponent<TipForClick>().CountDownCoroutine());
            }

            _IsGameFirstBegin = false;
            _HelpPanel.SetActive(false);
            
        });

    }
    
}
