using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipForAcu : MonoBehaviour
{
    public GameObject clickTipObject;//��ʾ����
    private string[] clickTip;//��ʾ��Ϣ
    public int clickTipNum;//��ʾ��Ϣ����
    public bool[] isClicked;//�Ƿ񱻵��
    public int[] clickQueue;//���˳��
    public int clickNum;//������ڼ���
    public bool _ClickAll;
    //ClickAnchor _ClickAnchor;
    ClickToAcupuncture _ClickToAcupuncture;

    public GameObject _AnchorNameHelp;//Ѩλ���ֽ���
    //public float _AnchorNameHelpMaxTime;//�����ʱ��
    //private float _AnchorNameHelpAliveTime;//����ʱ��
    public GameObject _CountDown;//����ʱ
    public int _CountDownTime;
    public Coroutine _Coroutine;

    private void Awake()
    {
        clickTip = new string[clickTipNum];
        isClicked = new bool[clickTipNum];
        clickQueue = new int[clickTipNum];
    }
    void Start()
    {
        //�Ƿ���������Ѩλ
        _ClickAll = false;

        for (int i = 0; i < clickTipNum; i++)
        {
            clickTip[i] = "" + (i + 1);
            isClicked[i] = false;
        }
        clickQueue[0] = 0;
        clickQueue[1] = 1;
        clickQueue[2] = 2;

        clickNum = 0;

        _ClickToAcupuncture = GameObject.Find("Anchor1").GetComponent<ClickToAcupuncture>();

        //_AnchorNameHelpAliveTime = 0f;
        _CountDownTime = 3;
        //_Coroutine = StartCoroutine(CountDownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CountDownTime == 0)
        {
            if (!isClicked[clickQueue[clickNum]])
            {
                ShowClickTip(clickQueue[clickNum]);
            }
            else
            {
                //clickNum���clickTipNum-1����Ϊ�±��Ǵ�0��ʼ��
                if (clickNum != clickTipNum - 1)
                {
                    clickNum++;
                }
                else
                {
                    _ClickAll = true;
                }
            }
        }

        IsShowAnchorNameHelp(_CountDownTime);

    }

    void ShowClickTip(int index)
    {
        clickTipObject.GetComponent<Text>().text = _ClickToAcupuncture._Anchor1Names[index];
    }

    public IEnumerator CountDownCoroutine()
    {
        while (_CountDownTime > 0)
        {
            _CountDown.GetComponent<Text>().text = _CountDownTime.ToString();
            yield return new WaitForSeconds(1f);
            _CountDownTime--;
        }
        Debug.Log("Time is up!");
    }

    void IsShowAnchorNameHelp(int flag)
    {
        if (flag == 0)
        {
            _AnchorNameHelp.SetActive(false);
            if (_Coroutine != null)
            {
                StopCoroutine(CountDownCoroutine());
            }
        }
    }
}
