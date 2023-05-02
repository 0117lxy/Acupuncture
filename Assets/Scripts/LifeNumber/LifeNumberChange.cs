using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeNumberChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lifeNumber;//生命值父GameObject
    public int maxHeartNumber;//最大生命值
    public int nowHeartNumber;//当前生命值
    private bool isChangeHeartNumber;//用于判断当前生命值是否改变，改变了为true，改变了才调用ShowHeartNumber()
    private GameObject[] heart;
    public UIManagerController _UIManagerController;

    public CameraShake _CameraShake;//相机晃动

    //test
    public int theHeartNumber;
    private void Awake()
    {
        //lifeNumber = this.GetComponent<Transform>();
        heart = new GameObject[maxHeartNumber];
        for (int i = 0; i < maxHeartNumber; ++i)
        {
            heart[i] = lifeNumber.GetChild(i).gameObject;
        }
        
    }
    void Start()
    {
        nowHeartNumber = maxHeartNumber;
        isChangeHeartNumber = true;

        //test
        theHeartNumber = maxHeartNumber;
    }

   
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            if(theHeartNumber != 0)
            {
                theHeartNumber--;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            if(theHeartNumber != 3)
            {
                theHeartNumber++;
            }
        }*/
        ChangeNowHeartNumber(theHeartNumber);

        if (isChangeHeartNumber)
        {
            ShowHeartNumber();
            Debug.Log("nowHeartNumber=" + nowHeartNumber.ToString());
        }
    }
    
    public void ChangeNowHeartNumber(int theHeartNumber)
    {
        if(nowHeartNumber != theHeartNumber)
        {
            //显示受伤特效
            _UIManagerController.ShowUpHurtImage();
            //相机晃动
            Debug.Log("is Change heart number???");
            _CameraShake.StartShake();

            nowHeartNumber = theHeartNumber;
            isChangeHeartNumber = true;
        }
    }

    private void ShowHeartNumber()
    {
        for(int i = 0; i < nowHeartNumber; ++i)
        {
            heart[i].SetActive(true); 
        }
        for(int i = nowHeartNumber; i < maxHeartNumber; ++i)
        {
            heart[i].SetActive(false);
        }
        isChangeHeartNumber=false;
    }
}
