using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;

public class Game03BeginManager : MonoBehaviour
{
    public GameObject _FirstBackground;//第一个背景
    private Image _FirstImage;//第一个背景的图片
    private float _CurrentFillNum = 1f;
    public float _TargetFillNum = 0f;
    public GameObject _TMPObject;//tmp物体
    public TMP_Text _TextMeshPro;//TMP组件
    //private TMP_Text _TMPText;
    private VerticalText _VerticalText; 

    public GameObject _HelpPanel;//帮助面板
    public  Button _HelpButton;
    Game03Help _Game03Help;

    public GameObject _AnchorNameForHelp;
    public GameObject _ClickTip;
    //TipForAcu _TipForAcu;
    TipForAcuDescirption _TipForAcuDescirption;
    //第一组穴位名
    public GameObject _Anchor1;
    //ClickToAcupuncture _ClickToAcupuncture;
    ClickToAcuDesription _ClickToAcuDesription;
    /*public GameObject _ClickTip;//点击提示
    public GameObject _AnchorNameForHelp;//穴位名
    TipForClick _TipForClick;*/

    //两个等待协程
    Coroutine coroutineText;
    Coroutine coroutineBackground;
    private bool _Init;

    private void Start()
    {
        _VerticalText = _TextMeshPro.GetComponent<VerticalText>();

        _FirstImage = _FirstBackground.GetComponent<Image>();

        _Game03Help = _HelpPanel.GetComponent<Game03Help>();

        //_ClickToAcupuncture = _Anchor1.GetComponent<ClickToAcupuncture>();

        _ClickToAcuDesription = _Anchor1.GetComponent<ClickToAcuDesription>();

        //_TipForAcu = _ClickTip.GetComponent<TipForAcu>();

        _TipForAcuDescirption = _ClickTip.GetComponent<TipForAcuDescirption>();

        _Init = false;
    }

    private void Update()
    {
        if(_FirstBackground.activeSelf != false && _VerticalText._IsShowAll == true)
        {
            
            coroutineText = StartCoroutine(WaitForTime(2f, () =>
            {
                Color color = _TextMeshPro.color;
                float a = color.a;
                if (a > 0f)
                {
                    a -= Time.deltaTime;
                    color.a = a;
                    _TextMeshPro.color = color;
                }
            }));

            coroutineBackground = StartCoroutine(WaitForTime(2.5f, () =>
            {
                if (_CurrentFillNum > _TargetFillNum)
                {
                    _CurrentFillNum -= Time.deltaTime;
                    _FirstImage.fillAmount = _CurrentFillNum;
                }
                else
                {
                    _FirstBackground.SetActive(false);
                    _TMPObject.SetActive(false);
                    //_HelpPanel.SetActive(true);
                }
            }));
            
        }

        if (_FirstBackground.activeSelf == false && _Init == false)
        {
            if(coroutineText != null)
            {
                StopCoroutine(coroutineText);
            }
            
            if(coroutineBackground != null)
            {
                StopCoroutine(coroutineBackground);
            }

            //显示穴位名字与倒计时。
            _ClickToAcuDesription.enabled = true;

            _ClickTip.SetActive(true);

            _AnchorNameForHelp.SetActive(true);

            _TipForAcuDescirption._Coroutine = StartCoroutine(_TipForAcuDescirption.CountDownCoroutine());

            _Init = true;
        }

    }

    

    //等待duration秒的方法
    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
