using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game03Help : MonoBehaviour
{
    public Button _Help;//������ť
    public Button _Continue;//������ť
    public GameObject _HelpPanel;

    public GameObject _ClickTip;//�����ʾ
    public GameObject _AnchorNameForHelp;//Ѩλ��
    //TipForClick _TipForClick;
    TipForAcu _TipForAcu;

    //��һ��Ѩλ��
    public GameObject _Anchor1;
    ClickToAcupuncture _ClickToAcupuncture;

    //public bool _IsFirstBegin;//�Ƿ��ǵ�һ�ο�ʼ

    void Start()
    {
        _Help.onClick.AddListener(OnHelpBtnClick);

        _Continue.onClick.AddListener(OnClickHelpBeginButton);

        //_TipForClick = _ClickTip.GetComponent<TipForClick>();

        _TipForAcu = _ClickTip.GetComponent<TipForAcu>();

        //_IsFirstBegin = true;

        _ClickToAcupuncture = _Anchor1.GetComponent<ClickToAcupuncture>();
    }

    void OnHelpBtnClick()
    {
        if (_HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
            _Continue.GetComponentInChildren<Text>().text = "����";
        }
    }

    //HelpPanel�ĵ���¼�������HelpPanelΪfalse
    void OnClickHelpBeginButton()
    {
        _HelpPanel.SetActive(false);
        
        /*//����ǵ�һ�ε���Ļ�����ʾѨλ��ʾ�͵���ʱ��
        if(_IsFirstBegin == true)
        {

            //��ʾѨλ�����뵹��ʱ��
            _ClickToAcupuncture.enabled = true;

            _ClickTip.SetActive(true);

            _AnchorNameForHelp.SetActive(true);

            _TipForAcu._Coroutine = StartCoroutine(_TipForAcu.CountDownCoroutine());

            _IsFirstBegin = false;
        }*/
    }

}
