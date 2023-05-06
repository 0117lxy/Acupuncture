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

    public MagicNiddle _MagicNiddle;//bag和niddle移动的脚本
    private int _SelectTime;//一共选择的组数

    public GameObject _HelpPanel;//帮助面板
    public Button _HelpButton;//帮助面板的Button

    public GameObject _TruePanel;//选择正确的面板
    public Button _TrueButton;//帮助面板的Button

    public GameObject _WinPanel;//胜利面板
    public Button _WinButton;//胜利按钮

    public GameObject _RewardPanel;
    public Button _RewardPanelButton;

    public GameObject _LosePanel;//失败面板
    public Button _LoseButton;//失败按钮

    public LifeNumberChange _LifeNumberChange;//控制生命值

    //public BagsEffect _BagsEffect;
    public float _DownDuration;//下降时间
    public float _OffsetY;//下降长度

    public int selectBag = -1;//选择了哪一个bag
    public int niddleIndex = 1;//niddle的位置index，分别有0、1、2三个

    private void Start()
    {
        //初始化选择的组
        GameSelectNiddle._NiddleNum = 0;
        _SelectTime = _MagicNiddle._Niddles.Length;

        if(_HelpPanel.activeSelf == false)
        {
            _HelpPanel.SetActive(true);
        }

        //帮助面板button的事件，隐藏HelpPanel，进行一次新的交换
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

        

        //选择正确的panel的Button的操作，隐藏TruePanel，开启下一次交换操作
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
            Debug.Log("开始交换了");
            StartSwag();
            _MagicNiddle._IsMoveToBottom = false;
        }

        //判断是否减少生命值的方法，但是一直在update中调用浪费资源，封装成方法，需要时调用一次。
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

        //判断胜利与失败
        //ToWin();
        ToLose();

    }

    //开始向下移动
    public void StartMoveToBottom()
    {
        _MagicNiddle._IsBagsCanMove = true;
    }

    //开始swag
    public void StartSwag()
    {
        _MagicNiddle._IsBagsCanSwag = true;
    }

    IEnumerator WaitForSeconds(float duartion)
    {
        yield return new WaitForSeconds(duartion);
    }

    //判断是否选择正确
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

    //胜利条件判定
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

    //失败条件判定
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
