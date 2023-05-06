using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectNiddle
{
    public static int _NiddleNum;
}

public class GameSelectNiddleManager : MonoBehaviour
{
    private string _BackScene;

    public MagicNiddle _MagicNiddle;//bag��niddle�ƶ��Ľű�
    private int _SelectTime;//һ��ѡ�������

    public GameObject _HelpPanel;//�������
    public Button _HelpButton;//��������Button

    public GameObject _TruePanel;//ѡ����ȷ�����
    public Button _TrueButton;//��������Button

    public GameObject _WinPanel;//ʤ�����
    public Button _WinButton;//ʤ����ť

    public GameObject _RewardPanel;
    public Button _RewardPanelButton;

    public GameObject _LosePanel;//ʧ�����
    public Button _LoseButton;//ʧ�ܰ�ť

    public LifeNumberChange _LifeNumberChange;//��������ֵ

    //public BagsEffect _BagsEffect;
    public float _DownDuration;//�½�ʱ��
    public float _OffsetY;//�½�����

    public int selectBag = -1;//ѡ������һ��bag
    public int niddleIndex = 1;//niddle��λ��index���ֱ���0��1��2����

    private void Start()
    {
        //��ʼ��ѡ�����
        GameSelectNiddle._NiddleNum = 0;
        _SelectTime = _MagicNiddle._Niddles.Length;

        if(_HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
        }

        //�������button���¼�������HelpPanel������һ���µĽ���
        _HelpButton.onClick.AddListener(delegate ()
        {
            if(_HelpPanel.activeSelf == true)
            {
                _HelpPanel.SetActive(false);

                if(GameSelectNiddle._NiddleNum < _SelectTime)
                {
                    _MagicNiddle.InitPos();
                    Invoke("StartMoveToBottom", 1f);
                }
            }
        });

        

        //ѡ����ȷ��panel��Button�Ĳ���������TruePanel��������һ�ν�������
        _TrueButton.onClick.AddListener(delegate ()
        {
            if(_TruePanel.activeSelf == true)
            {
                _TruePanel.SetActive(false);
                _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].SetActive(false);
                GameSelectNiddle._NiddleNum++; 
                Debug.Log("NiddleNum is :" + GameSelectNiddle._NiddleNum);
                if (GameSelectNiddle._NiddleNum < _SelectTime)
                {
                    _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].SetActive(true);
                    _MagicNiddle.InitPos();
                    Invoke("StartMoveToBottom", 1f);
                }
            }
        });

        _BackScene = "Game00";

        _WinButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });

        _RewardPanelButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });

        _LoseButton.onClick.AddListener(delegate ()
        {
            Globe._NextSceneName = _BackScene;
            SceneManager.LoadScene("Loading");
        });
    }

    private void Update()
    {
        if (_MagicNiddle._IsMoveToBottom == true)
        {
            Debug.Log("��ʼ������");
            StartSwag();
            _MagicNiddle._IsMoveToBottom = false;
        }

        //�ж��Ƿ��������ֵ�ķ���������һֱ��update�е����˷���Դ����װ�ɷ�������Ҫʱ����һ�Ρ�
        /*for(int i = 0; i < _MagicNiddle._Bags.Length; i++)
        {
            if(_MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.y == _BagsEffect._OffsetY
                && _MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.x 
                == _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition.x)
            {
                _TruePanel.SetActive(true);
            }
            if (_MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.y == _BagsEffect._OffsetY
                && _MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.x
                != _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition.x)
            {
                _LifeNumberChange.theHeartNumber--;
            }
        }*/

        //�ж�ʤ����ʧ��
        //ToWin();
        ToLose();

    }

    //��ʼ�����ƶ�
    public void StartMoveToBottom()
    {
        _MagicNiddle._IsBagsCanMove = true;
    }

    //��ʼswag
    public void StartSwag()
    {
        _MagicNiddle._IsBagsCanSwag = true;
    }

    IEnumerator WaitForSeconds(float duartion)
    {
        yield return new WaitForSeconds(duartion);
    }

    //�ж��Ƿ�ѡ����ȷ
    public void IsSelectRight()
    {
        /*for (int i = 0; i < _MagicNiddle._Bags.Length; i++)
        {
            if (_MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.y == offset
                && _MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.x
                == _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition.x)
            {
                if(GameSelectNiddle._NiddleNum < _SelectTime-1)
                {
                    _TruePanel.SetActive(true);
                }
                else
                {
                    _WinPanel.SetActive(true);
                }
            }
            if (_MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.y == offset
                && _MagicNiddle._Bags[i].GetComponent<RectTransform>().anchoredPosition.x
                != _MagicNiddle._Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition.x)
            {
                _LifeNumberChange.theHeartNumber--;
                StartCoroutine(BagsDown(_MagicNiddle._Bags[i].GetComponent<RectTransform>()));
            }
        }*/

        if(selectBag == niddleIndex)
        {
            selectBag = -1;
            niddleIndex = 1;
            if (GameSelectNiddle._NiddleNum < _SelectTime - 1)
            {
                _TruePanel.SetActive(true);
            }
            else
            {
                ToWin();
            }
            /*else
            {
                _WinPanel.SetActive(true);
            }*/
        }  
        else
        {
            _LifeNumberChange.theHeartNumber--;
            StartCoroutine(BagsDown(_MagicNiddle._Bags[selectBag].GetComponent<RectTransform>()));
            selectBag = -1;
        }
    }

    IEnumerator BagsDown(RectTransform rect)
    {
        float t = 0;
        Vector3 startPos = rect.anchoredPosition;
        //Vector3 endPos = startPos + new Vector3(0, _OffsetY, 0);
        while (t < _DownDuration)
        {
            t += Time.deltaTime;
            float y = Mathf.SmoothStep(0, -_OffsetY, t / _DownDuration);
            Vector3 pos = startPos + new Vector3(0, y, 0);
            rect.anchoredPosition = pos;
            yield return null;
        }

        yield break;
    }

    //ʤ�������ж�
    void ToWin()
    {
        /*if(GameSelectNiddle._NiddleNum == 3 && _LifeNumberChange.nowHeartNumber > 0)
        {
            *//*if (_WinPanel.activeSelf == false)
            {
                _WinPanel.SetActive(true);
            }*//*
            if (_RewardPanel.activeSelf == false)
            {
                _RewardPanel.SetActive(true);
                Reward._IsHaveReward[1] = true;
            }
        }*/

        if (_RewardPanel.activeSelf == false)
        {
            _RewardPanel.SetActive(true);
            Reward._IsHaveReward[1] = true;
        }
    }

    //ʧ�������ж�
    void ToLose()
    {
        if(_LifeNumberChange.nowHeartNumber == 0)
        {
            if(_LosePanel.activeSelf == false)
            {
                _LosePanel.SetActive(true);
            }
        }
    }
}
