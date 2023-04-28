using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowClickedAnchorName : MonoBehaviour
{
    public float _ShowUpSpeed = 50;//上升速度（可以在面板上设置）
    private float _Speed = 0;//上升速度
    private Vector2 _ShowUpUIPosition;//显示在UI上的位置
    public float _DestroyTime = 0.4f;//穴位名字销毁时间
    //public GameObject _ShowUpAnchorNameObject;
    // Start is called before the first frame update
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //修改穴位名位置（上移）
        float offset = _Speed * Time.deltaTime;
        _ShowUpUIPosition += new Vector2(0, offset);
        this.transform.position = _ShowUpUIPosition;
    }
    
    //销毁
    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    //设置text为穴位名，设置初始位置与速度
    public void ShowUp(string anchorName, Vector2 uiPosition)
    {
        Text _TextObject = this.gameObject.GetComponent<Text>();
        _TextObject.text = anchorName;
        _Speed = _ShowUpSpeed;
        _ShowUpUIPosition = uiPosition;
        Invoke("DestroySelf", _DestroyTime);
    }
}
