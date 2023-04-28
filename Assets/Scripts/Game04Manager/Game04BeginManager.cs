using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game04BeginManager : MonoBehaviour
{
    //三个tmp组件
    public GameObject[] _TextMeshPro;

    //当前tmp竖排文字脚本组件
    private VerticalText currentVerticalTextScript;

    //当前显示脚本
    private int currentShowIndex;

    //销毁时间
    public float _DestroyTime;

    //是否显示
    //public int _TMPNum;
    private bool[] _IsShowTMP;

    //医生的Image
    public Image _Image;
    Color currentColor;
    float currentAlpha;
    public float _AlphaSpeed;

    //点击button
    public Button _BackButton;
    public GameObject _ButtonText; 
    private string _NextScene;

    private void Start()
    {
        //初始化是否显示的布尔数组
        Init();


        _NextScene = "Game00";
        //给button注册事件
        _BackButton.onClick.AddListener(OnButtonClick);

        //给图片设置透明度
        currentColor = _Image.color;
        currentAlpha = 0;
        currentColor.a = 0;
        _Image.color = currentColor;
    }

    private void Update()
    {
        ShowText();

        ShowImage();
    }

    //初始化
    void Init()
    {

        _IsShowTMP = new bool[_TextMeshPro.Length];
        for (int i = 0; i < _TextMeshPro.Length; i++)
        {
            _IsShowTMP[i] = false;
        }

        currentShowIndex = 0;
    }
    

    //显示字体
    private void ShowText()
    {
        if(currentShowIndex < _TextMeshPro.Length)
        {
            currentVerticalTextScript = _TextMeshPro[currentShowIndex].GetComponent<VerticalText>();

            if(currentVerticalTextScript != null)
            {
                _TextMeshPro[currentShowIndex].SetActive(true);
            }

            if(currentVerticalTextScript != null && currentVerticalTextScript._IsShowAll == true && currentShowIndex != _TextMeshPro.Length - 1)
            {
                currentVerticalTextScript.DestroySelf(_DestroyTime);
            }

            if(currentVerticalTextScript != null && currentVerticalTextScript._IsDestroy == true)
            {
                _TextMeshPro[currentShowIndex].SetActive(false);

                currentShowIndex++;
            }

            if(currentVerticalTextScript != null && currentVerticalTextScript._IsShowAll == true && currentShowIndex == _TextMeshPro.Length - 1)
            {
                _ButtonText.GetComponent<Text>().text = ">>>返回";
            }       
        }
    }

    //显示老医生Image
    void ShowImage()
    {
        if(currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime * _AlphaSpeed;
            currentColor.a = currentAlpha;
            _Image.color = currentColor;
        }
    }

    //点击返回到主界面
    private void OnButtonClick()
    {
        Globe._NextSceneName = _NextScene;
        SceneManager.LoadScene("Loading");
    }
}
