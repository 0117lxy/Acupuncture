using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipForClick : MonoBehaviour
{

    public GameObject clickTipObject;//��ʾ����

    public int clickTipNum;//��ʾ��Ϣ����
    public bool[,] isClicked;//�Ƿ񱻵��
    public int[] clickQueue;//���˳��
    public int clickNum;//������ڼ���
    public bool[] _ClickAll;//ĳһ��Ѩλ�Ƿ�����
    public ClickAnchor[] _ClickAnchor;//ĳһ��Ѩλ�ĵ���ű�

    public GameObject[] _AnchorNameHelp;//Ѩλ���ֽ���

    public GameObject[] _CountDown;//����ʱ
    public int _CountDownTime;
    public Coroutine _Coroutine;
    
    private void Awake()
    {
        isClicked = new bool[3, clickTipNum];
        clickQueue = new int[clickTipNum];
        _ClickAll = new bool[clickTipNum];

        //_ClickAnchor = new ClickAnchor[3];
    }

    void Start()
    {

        //��ʼ��
        for (int i = 0; i < clickTipNum; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                isClicked[j,i] = false;
            }
            clickQueue[i] = i;
            //isClicked[i] = false;
            _ClickAll[i] = false;
        }

        clickNum = 0;

        _CountDownTime = 3;

    }

    void Update()
    {

        ShowClickQueueTip(_CountDownTime);

        IsShowAnchorNameHelp(_CountDownTime);

    }

    void ShowClickTip(int index)
    {
        //Debug.Log("Game01._NowLevel : " + Game01._NowLevel);
        clickTipObject.GetComponent<Text>().text = _ClickAnchor[Game01._NowLevel]._Anchor1Names[index];
       
    }

    public IEnumerator CountDownCoroutine()
    {
        while (_CountDownTime > 0)
        {
            Debug.Log("_CountDownTime is :" + _CountDownTime);
            _CountDown[Game01._NowLevel].GetComponent<Text>().text = _CountDownTime.ToString();
            yield return new WaitForSeconds(1f);
            _CountDownTime--;
        }
        Debug.Log("Time is up!");
    }

    //�Ƿ�չʾѨλ��ʾ
    void IsShowAnchorNameHelp(int flag)
    {
        if(flag == 0)
        {
            _AnchorNameHelp[Game01._NowLevel].SetActive(false);
            if(_Coroutine != null)
            {
                StopCoroutine(CountDownCoroutine());
                _Coroutine = null;
            }
        }
    }

    //��ʾ�����ʾ�����е���ʾ
    void ShowClickQueueTip(int flag)
    {
        if(flag == 0)
        {
            if (isClicked[Game01._NowLevel, clickQueue[clickNum]] == false)
            {
                //Debug.Log("ClickNum: " + clickNum);
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
                    _ClickAll[Game01._NowLevel] = true;
                    //clickTipObject.GetComponent<Text>().text = "";
                    //clickNum = clickNum % clickTipNum;
                }
            }
        }
    }

}
