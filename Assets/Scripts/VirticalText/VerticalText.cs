using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class VerticalText : MonoBehaviour
{
    public float _DelayTime = 0.1f;//延迟时间
    private float _Timer = 0.0f;//计时器
    public string[] _FullText;//全部文本

    private int _FullLines;//总计行数
    private int _VisibleLines;//可见行数
    public bool _IsShowAll = false;//是否全部显示

    private TMP_Text _Text;//tmp的text

    //销毁时间
    public float _DestroyTime;
    public bool _IsDestroy = false;

    private void Start()
    {
        _Text = this.GetComponent<TMP_Text>();
        _VisibleLines = 0;

        _FullLines = _FullText.Length;
    }

    private void Update()
    {
        if(_VisibleLines < _FullLines)
        {
            //开始计时
            _Timer += Time.deltaTime;

            if (_Timer > _DelayTime)
            {
                if (_VisibleLines != _FullLines - 1)
                {
                    _Text.text += _FullText[_VisibleLines] + "\n";
                }
                else
                {
                    _Text.text += _FullText[_VisibleLines];
                }

                _VisibleLines++;
                _Timer = 0.0f;
            }
        }
        else
        {
            _IsShowAll = true;
        }
    }

    public void DestroySelf(float duration)
    {
        float a;
        Color color = _Text.color;
        a = color.a;

        Coroutine coroutine = StartCoroutine(WaitForTime(duration, () =>
        {

            color = _Text.color;
            a = color.a;
            if (a > 0f)
            {
                a -= Time.deltaTime;
                color.a = a;
                _Text.color = color;
            }

        }));

        if(a <= 0f)
        {
            StopCoroutine(coroutine);
            _IsDestroy = true;
            GameObject obj = gameObject.GetComponent<TMP_Text>().gameObject;
            obj.SetActive(false);
        }
    }

    //等待duration秒的方法
    IEnumerator WaitForTime(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
