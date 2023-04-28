using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnchortTag : MonoBehaviour, IPointerEnterHandler
{
    public string _Tag;
    
    //�ýű����ڷ������ָ����ͣbutton��tag
    public void OnPointerEnter(PointerEventData eventData)
    {
        Button button = GetComponent<Button>();
        if(button != null)
        {
            _Tag = button.tag;
            Debug.Log("��ǰ�����ͣ�İ�ť��Tag�ǣ�" + _Tag);
        }
    }
}
