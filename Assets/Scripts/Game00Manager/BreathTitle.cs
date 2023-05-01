using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathTitle : MonoBehaviour
{
    public float _Speed = 2.0f; // 颜色渐变速度
    public Color _StartColor;   // 起始颜色
    public Color _EndColor;     // 终止颜色

    public Image _Image;
    private bool reverse = false; // 颜色渐变方向是否反转

    public float _Precision = 0.0001f;

    void Start()
    {
        _Image = GetComponent<Image>();
        StartCoroutine(Breath()); // 启动呼吸灯协程
    }

    IEnumerator Breath()
    {
        while (true)
        {
            //使用Mathf.Pingpong让一个值在0和长度之间来回循环。
            if (!reverse)
            {
                _Image.color = Color.Lerp(_StartColor, _EndColor, Mathf.PingPong(Time.time * _Speed, 1));
            }
            else
            {
                _Image.color = Color.Lerp(_EndColor, _StartColor, Mathf.PingPong(Time.time * _Speed, 1));
            }
            if (Mathf.Abs(_Image.color.r - _EndColor.r) < _Precision &&
                Mathf.Abs(_Image.color.g - _EndColor.g) < _Precision &&
                Mathf.Abs(_Image.color.b - _EndColor.b) < _Precision)
            {
                reverse = true;
            }
            else if (Mathf.Abs(_Image.color.r - _StartColor.r) < _Precision &&
                     Mathf.Abs(_Image.color.g - _StartColor.g) < _Precision &&
                     Mathf.Abs(_Image.color.b - _StartColor.b) < _Precision)
            {
                reverse = false;
            }
            yield return null;
        }
    }
}
