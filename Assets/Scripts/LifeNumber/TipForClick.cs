using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TipForClick : MonoBehaviour
{

    public GameObject clickTipObject;//提示物体

    public int clickTipNum;//提示信息数量
    public bool[,] isClicked;//是否被点击
    public int[] clickQueue;//点击顺序
    public int clickNum;//点击到第几个
    public bool[] _ClickAll;//某一套穴位是否点击完
    public ClickAnchor[] _ClickAnchor;//某一套穴位的点击脚本

    public GameObject[] _AnchorNameHelp;//穴位名字介绍

    public GameObject[] _CountDown;//倒计时
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

        //初始化
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

    //是否展示穴位提示
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

    //显示点击提示序列中的提示
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
                //clickNum最大到clickTipNum-1，因为下标是从0开始的
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
