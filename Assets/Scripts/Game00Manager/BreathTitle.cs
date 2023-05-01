using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathTitle : MonoBehaviour
{
    public float _Speed = 2.0f; // ��ɫ�����ٶ�
    public Color _StartColor;   // ��ʼ��ɫ
    public Color _EndColor;     // ��ֹ��ɫ

    public Image _Image;
    private bool reverse = false; // ��ɫ���䷽���Ƿ�ת

    public float _Precision = 0.0001f;

    void Start()
    {
        _Image = GetComponent<Image>();
        StartCoroutine(Breath()); // ����������Э��
    }

    IEnumerator Breath()
    {
        while (true)
        {
            //ʹ��Mathf.Pingpong��һ��ֵ��0�ͳ���֮������ѭ����
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
