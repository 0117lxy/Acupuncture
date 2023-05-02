using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagsEffect : MonoBehaviour
{
    public Image _Image;
    public Material _HoverMaterial;//��Ե�����
    public float _EdgeMaxWidth = 0.1f;//����Ե����
    public Color _EdgeColor = Color.white;//��Ե����ɫ
    public float _EdgeDuration = 1f;//������Ե���ʱ��
    private bool _IsHovering = false;
    private float t;

    private Material originalMaterial;//��ʼ����

    public float _OffsetY;//��������ϵ�λ��
    public float _UpDuration;//����λ�Ƶ�ʱ��

    public GameSelectNiddleManager _GameSelectNiddleManager;

    private void Start()
    {
        originalMaterial = _Image.material;
    }

    void Update()
    {
        if(_IsHovering)
        {
            StartCoroutine(ChangeEdgeWidth());
        }
    }

    public void OnPointerEnter()
    {
        Debug.Log("��ʼ��ʾ��Ե�⣡");
        _Image.material = _HoverMaterial;
        _Image.material.SetColor("lineColor", _EdgeColor);
        _IsHovering = true;
        t = 0f;
    }

    public void OnPointerExit()
    {
        _Image.material = originalMaterial;
        _IsHovering = false;
        _Image.material.SetFloat("_lineWidth", 0);
        t = 0f;
    }

    public void OnPointer1Down()
    {
        _GameSelectNiddleManager.selectBag = 0;
        StartCoroutine(BagUp());
        
    }

    public void OnPointer2Down()
    {
        _GameSelectNiddleManager.selectBag = 1;
        StartCoroutine(BagUp());
        
    }

    public void OnPointer3Down()
    {
        _GameSelectNiddleManager.selectBag = 2;
        StartCoroutine(BagUp());
        
    }

    //�ı��Ե���ȵ�Э��
    IEnumerator ChangeEdgeWidth()
    {
        while (t < _EdgeDuration)
        {
            t += Time.deltaTime;
            float lineWidth = Mathf.SmoothStep(0, _EdgeMaxWidth, t / _EdgeDuration);
            _Image.material.SetFloat("_lineWidth", lineWidth);
            yield return null;
        }

        yield break;
    }

    IEnumerator BagUp()
    {
        float t = 0;
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 startPos = rect.anchoredPosition;
        //Vector3 endPos = startPos + new Vector3(0, _OffsetY, 0);
        while (t < _UpDuration)
        {
            t += Time.deltaTime;
            float y = Mathf.SmoothStep(0, _OffsetY, t / _UpDuration);
            Vector3 pos = startPos + new Vector3(0, y, 0);
            rect.anchoredPosition = pos;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        _GameSelectNiddleManager.IsSelectRight();

        yield break;
    }

}
