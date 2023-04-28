using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game04BeginManager : MonoBehaviour
{
    //����tmp���
    public GameObject[] _TextMeshPro;

    //��ǰtmp�������ֽű����
    private VerticalText currentVerticalTextScript;

    //��ǰ��ʾ�ű�
    private int currentShowIndex;

    //����ʱ��
    public float _DestroyTime;

    //�Ƿ���ʾ
    //public int _TMPNum;
    private bool[] _IsShowTMP;

    //ҽ����Image
    public Image _Image;
    Color currentColor;
    float currentAlpha;
    public float _AlphaSpeed;

    //���button
    public Button _BackButton;
    public GameObject _ButtonText; 
    private string _NextScene;

    private void Start()
    {
        //��ʼ���Ƿ���ʾ�Ĳ�������
        Init();


        _NextScene = "Game00";
        //��buttonע���¼�
        _BackButton.onClick.AddListener(OnButtonClick);

        //��ͼƬ����͸����
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

    //��ʼ��
    void Init()
    {

        _IsShowTMP = new bool[_TextMeshPro.Length];
        for (int i = 0; i < _TextMeshPro.Length; i++)
        {
            _IsShowTMP[i] = false;
        }

        currentShowIndex = 0;
    }
    

    //��ʾ����
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
                _ButtonText.GetComponent<Text>().text = ">>>����";
            }       
        }
    }

    //��ʾ��ҽ��Image
    void ShowImage()
    {
        if(currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime * _AlphaSpeed;
            currentColor.a = currentAlpha;
            _Image.color = currentColor;
        }
    }

    //������ص�������
    private void OnButtonClick()
    {
        Globe._NextSceneName = _NextScene;
        SceneManager.LoadScene("Loading");
    }
}
