using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnchortTag : MonoBehaviour, IPointerEnterHandler
{
    public string _Tag;
    
    //该脚本用于返回鼠标指针悬停button的tag
    public void OnPointerEnter(PointerEventData eventData)
    {
        Button button = GetComponent<Button>();
        if(button != null)
        {
            _Tag = button.tag;
            Debug.Log("当前鼠标悬停的按钮的Tag是：" + _Tag);
        }
    }
}
