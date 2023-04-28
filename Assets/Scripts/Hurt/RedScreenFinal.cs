using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedScreenFinal : MonoBehaviour
{
    public Image _HurtImage;//受伤图片
    private Color _HurtColor;//受伤颜色

    //透明度
    public float _MaxAlpha;
    public float _MinAlpha;
    private float currentAlpha;

    public bool _HurtVisible = false;


    private void Start()
    {
        currentAlpha = _MinAlpha;
        _HurtColor = _HurtImage.color;
    }

    private void Update()
    {
        if(currentAlpha < _MaxAlpha && _HurtVisible == true)
        {
            currentAlpha += Time.deltaTime;
            _HurtColor = _HurtImage.color;
            _HurtColor.a = currentAlpha;
            _HurtImage.color = _HurtColor;
        }
        if(currentAlpha > _MaxAlpha)
        {
            _HurtVisible = false;
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void ShowUp()
    {
        _HurtColor = _HurtImage.color;
        _HurtColor.a = currentAlpha;
        _HurtImage.color = _HurtColor;
        _HurtVisible = true;
    }
}
