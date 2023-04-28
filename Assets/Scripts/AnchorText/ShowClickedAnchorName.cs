using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowClickedAnchorName : MonoBehaviour
{
    public float _ShowUpSpeed = 50;//�����ٶȣ���������������ã�
    private float _Speed = 0;//�����ٶ�
    private Vector2 _ShowUpUIPosition;//��ʾ��UI�ϵ�λ��
    public float _DestroyTime = 0.4f;//Ѩλ��������ʱ��
    //public GameObject _ShowUpAnchorNameObject;
    // Start is called before the first frame update
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�޸�Ѩλ��λ�ã����ƣ�
        float offset = _Speed * Time.deltaTime;
        _ShowUpUIPosition += new Vector2(0, offset);
        this.transform.position = _ShowUpUIPosition;
    }
    
    //����
    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    //����textΪѨλ�������ó�ʼλ�����ٶ�
    public void ShowUp(string anchorName, Vector2 uiPosition)
    {
        Text _TextObject = this.gameObject.GetComponent<Text>();
        _TextObject.text = anchorName;
        _Speed = _ShowUpSpeed;
        _ShowUpUIPosition = uiPosition;
        Invoke("DestroySelf", _DestroyTime);
    }
}
