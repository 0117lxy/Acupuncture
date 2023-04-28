using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game02BeginManager : MonoBehaviour
{
    public GameObject _HelpPanel;
    public Button _HelpPanelBeginButton;//��������Ŀ�ʼ��Ϸ��ť��֮����ɼ�����Ϸ��ť
    public bool _IsGameFirstBegin = true;

    //��������ָ��
    public GameObject _TipInfo;
    TipInfoController _TipInfoController;

    /*GameObject _CountDown;
    GameObject _ClickTip;*/

    //���ڿ�ʼ�ڶ���
    public GameObject _Anchor1;//��һ��Ѩλ
    ClickAnchor _ClickAnchor1;
    ClickToAcupuncture _ClickToAcupuncture;
    public GameObject _AnchorNameHelp;//Ѩλ��
    public GameObject _ClickTip;//��ʾ��Ҫ�����Ѩλ��������
    //TipForClick _TipForClick;
    TipForAcu _TipForAcu;

    //��ʼ��ϰ��Ϸ����ʾ
    public GameObject _BeginTest;
    private bool _IsShowBeginTest = false;
    Coroutine _BeginCoroutine;

    //�ؿ�2��ʼʱ����_HelpPanel��ActiveΪTrue
    private void Start()
    {
        //_TipInfo = GameObject.Find("TipInfo");

        _TipInfoController = _TipInfo.GetComponent<TipInfoController>();

        if (_IsGameFirstBegin == true && _HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
        }

        _HelpPanelBeginButton.onClick.AddListener(OnClickHelpBeginButton);

        //_ClickAnchor1 = _Anchor1.GetComponent<ClickAnchor>();
        _ClickToAcupuncture = _Anchor1.GetComponent<ClickToAcupuncture>();

        //_TipForClick = _ClickTip.GetComponent<TipForClick>();
        
        _TipForAcu = _ClickTip.GetComponent<TipForAcu>();   
    }

    private void Update()
    {

        //ֻ��ʾһ�ο�ʼ��ϰ����ʾ
        if (_TipInfoController._IsShowAllTips == true && _BeginTest.activeSelf == false && _IsShowBeginTest == false)
        {
            if(_BeginCoroutine == null)
            {
                _BeginCoroutine = StartCoroutine(WaitForTime(2f, () =>
                {
                    _BeginTest.SetActive(true);
                    _IsShowBeginTest = true;
                    Invoke("HideTipAndShowDetails", 1f);

                }));
            }
            
        }

    }

    //HelpPanel�ĵ���¼�������HelpPanelΪfalse
    void OnClickHelpBeginButton()
    {
        _IsGameFirstBegin = false;
        _HelpPanel.SetActive(false);

        _TipInfoController._Tips[0].SetActive(true);
    }

    //��ʾѨλ��Ѩλ���֡���ʾ����Ϣ
    void ShowGameDetails()
    {
        if (_TipInfoController._IsShowAllTips == true)
        {

            _ClickToAcupuncture.enabled = true;

            _ClickTip.SetActive(true);

            //��ʾѨλ
            for (int i = 0; i < _ClickToAcupuncture.anchor1Number; i++)
            {
                if (_Anchor1.transform.GetChild(i).gameObject.activeSelf == false)
                {
                    _Anchor1.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            _AnchorNameHelp.SetActive(true);

        }
    }

    //���ٿ�ʼ��ϰ��Ϸ����ʾ��չʾ��Ϸ�ؿ����壬��ʼ��ϰ��Ϸ
    void HideTipAndShowDetails()
    {
        _BeginTest.SetActive(false);

        ShowGameDetails();

        _TipForAcu._Coroutine = StartCoroutine(_TipForAcu.CountDownCoroutine());
    }

    //�ȴ�duration��ķ���
    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
