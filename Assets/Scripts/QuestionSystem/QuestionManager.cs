using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameQuestion
{
    public static int _QuestionNum;//现在是第几个问题
}

public class QuestionManager : MonoBehaviour
{
    Questions _Questions;//问题
    public Button _QuestionButton;//界面上打开问题面板的Button

    public GameObject _QuestionRolePanel;//引导进行答题的人的面板
    public Button _QuestionRoleButton;//引导进行答题的人的面板的Button

    public GameObject _QuestionPanel;//问题面板
    public GameObject _QuestionObj;//问题组件
    public GameObject[] _AnswersObj;//答案组件
    public Button _AnswerButton;//问题面板上确定答案的button
    public GameObject _RightAnswerText;//正确答案的组件
    public Button _CloseButton;//关闭问题面板的Button

    public GameObject _TruePanel;//正确面板
    public Button _TrueButton;//正确按钮

    public GameObject _WrongPanel;//错误面板
    public Button _WrongButton;//错误按钮

    public GameObject _RewardPanel;//奖励面板
    public Button _RewardButton;//奖励按钮

    private int currentQuestionIndex = 0; // 当前问题索引
    private int selectedAnswerIndex = -1; // 用户选择的答案索引

    public GameObject _RightSignature;//正确的图片
    private float _OffsetX;//正确图片应该在的位置

    private void Start()
    {
        _Questions = GetComponent<Questions>();

        GameQuestion._QuestionNum = 0;

        //点击打开问题面板
        _QuestionButton.onClick.AddListener(delegate ()
        {
            if (_QuestionRolePanel.activeSelf == false)
            {
                _QuestionRolePanel.SetActive(true);
            }
        });

        //点击打开问题系统
        _QuestionRoleButton.onClick.AddListener(delegate ()
        {
            if (_QuestionRolePanel.activeSelf == true)
            {
                _QuestionRolePanel.SetActive(false);
            }

            if (_QuestionPanel.activeSelf == false)
            {
                _QuestionPanel.SetActive(true);
            }
        });

        //问题面板上确定答案的button点击后提交答案
        _AnswerButton.onClick.AddListener(delegate ()
        {
            SubmitAnswer();
        });

        _CloseButton.onClick.AddListener(delegate ()
        {
            if(_QuestionPanel.activeSelf == true)
            {
                _QuestionPanel.SetActive(false);
            }
        });

        //正确选择了答案的panel的button点击事件
        _TrueButton.onClick.AddListener(delegate ()
        {
            if (_TruePanel.activeSelf == true)
            {
                _TruePanel.SetActive(false);
            }
            if(_RightAnswerText.activeSelf == true)
            {
                _RightAnswerText.SetActive(false);  
            }
            if (_RightSignature.activeSelf == true)
            {
                _RightSignature.SetActive(false);
            }
            GameQuestion._QuestionNum++;
            ShowCurrentQuestion();
        });

        //错误选择了答案的panel的button点击事件
        _WrongButton.onClick.AddListener(delegate ()
        {
            if (_WrongPanel.activeSelf == true)
            {
                _WrongPanel.SetActive(false);
            }

            if(_RightSignature.activeSelf == true)
            {
                _RightSignature.SetActive(false);
            }
        });

        //选择完所有答案，获得奖励的面板的button
        _RewardButton.onClick.AddListener(delegate ()
        {
            if (_RewardPanel.activeSelf == true)
            {
                _RewardPanel.SetActive(false);
            }

            if(_QuestionPanel.activeSelf == true)
            {
                _QuestionPanel.SetActive(false);
            }
        });

        //InitQuestion();
        ShowCurrentQuestion();
    }

    void ShowCurrentQuestion()
    {
        //声明现在是哪一题
        currentQuestionIndex = GameQuestion._QuestionNum;

        _QuestionObj.GetComponent<Text>().text = _Questions.questions[currentQuestionIndex].questionName;

        for(int i = 0; i < _AnswersObj.Length; i++)
        {
            //_AnswersObj[i].GetComponent<Text>().text = (char)('A'+(char)i) + '.' + _Questions.questions[GameQuestion._QuestionNum].answers[i];
            //字符串插值
            string answerText = $"{(char)('A' + i)}.{_Questions.questions[currentQuestionIndex].answers[i]}";
            _AnswersObj[i].GetComponent<Text>().text = answerText;
        }
    }

    //选择了哪个答案，设置选择的index
    public void AnswerSelected(int index)
    {
        selectedAnswerIndex = index;
    }

    //提交答案
    public void SubmitAnswer()
    {

        //如果已经回答完所有的问题
        if(currentQuestionIndex  == _Questions.questions.Length - 1)
        {
            if(selectedAnswerIndex == _Questions.questions[currentQuestionIndex].correctAnswerIndex)
            {
                if (_RewardPanel.activeSelf == false)
                {
                    _RewardPanel.SetActive(true);
                }
                if (_RightAnswerText.activeSelf == false)
                {
                    _RightAnswerText.GetComponent<Text>().text = "正确答案：" + $"{(char)('A' + _Questions.questions[currentQuestionIndex].correctAnswerIndex)}";
                    _RightAnswerText.SetActive(true);
                }
                //设置奖励陈列馆中对应位置的奖励为true，表示已经获得该奖励
                Reward._IsHaveReward[1] = true;
            }
            else if(selectedAnswerIndex != _Questions.questions[currentQuestionIndex].correctAnswerIndex)
            {
                // 回答错误，显示惩罚面板
                if (_WrongPanel.activeSelf == false)
                {
                    _WrongPanel.SetActive(true);
                }
            }
            
        }

        else if (selectedAnswerIndex == _Questions.questions[currentQuestionIndex].correctAnswerIndex)
        {
            // 回答正确，显示奖励面板
            if(_TruePanel.activeSelf == false)
            {
                _TruePanel.SetActive(true);
            }
            if(_RightAnswerText.activeSelf == false)
            {
                _RightAnswerText.GetComponent<Text>().text = "正确答案：" + $"{(char)('A' + _Questions.questions[currentQuestionIndex].correctAnswerIndex)}";
                _RightAnswerText.SetActive(true);
            }

            
        }
        else
        {
            // 回答错误，显示惩罚面板
            if (_WrongPanel.activeSelf == false)
            {
                _WrongPanel.SetActive(true);
            }
        }

        // 重置选择的答案索引
        selectedAnswerIndex = -1;
    }

    //选择了哪个答案，显示对号图片
    public void OnPointer1Down()
    {
        Debug.Log("选择了第一个答案");
        AnswerSelected(0);
        float width1 = _AnswersObj[0].GetComponent<RectTransform>().rect.width;
        float x = _AnswersObj[0].GetComponent<RectTransform>().anchoredPosition.x;
        float y = _AnswersObj[0].GetComponent<RectTransform>().anchoredPosition.y;
        float width2 = _RightSignature.GetComponent<RectTransform>().rect.width;
        Vector3 pos = Vector3.zero;
        pos.x = x - width1 / 2 - width2 / 2;
        pos.y = y;
        _RightSignature.GetComponent<RectTransform>().anchoredPosition = pos;
        _RightSignature.SetActive(true);
    }

    public void OnPointer2Down()
    {
        Debug.Log("选择了第二个答案");
        AnswerSelected(1);
        float width1 = _AnswersObj[1].GetComponent<RectTransform>().rect.width;
        float x = _AnswersObj[1].GetComponent<RectTransform>().anchoredPosition.x;
        float y = _AnswersObj[1].GetComponent<RectTransform>().anchoredPosition.y;
        float width2 = _RightSignature.GetComponent<RectTransform>().rect.width;
        Vector3 pos = Vector3.zero;
        pos.x = x - width1 / 2 - width2 / 2;
        pos.y = y;
        _RightSignature.GetComponent<RectTransform>().anchoredPosition = pos;
        _RightSignature.SetActive(true);
    }
    public void OnPointer3Down()
    {
        Debug.Log("选择了第三个答案");
        AnswerSelected(2);
        float width1 = _AnswersObj[2].GetComponent<RectTransform>().rect.width;
        float x = _AnswersObj[2].GetComponent<RectTransform>().anchoredPosition.x;
        float y = _AnswersObj[2].GetComponent<RectTransform>().anchoredPosition.y;
        float width2 = _RightSignature.GetComponent<RectTransform>().rect.width;
        Vector3 pos = Vector3.zero;
        pos.x = x - width1 / 2 - width2 / 2;
        pos.y = y;
        _RightSignature.GetComponent<RectTransform>().anchoredPosition = pos;
        _RightSignature.SetActive(true);
    }
    public void OnPointer4Down()
    {
        Debug.Log("选择了第四个答案");
        AnswerSelected(3);
        float width1 = _AnswersObj[3].GetComponent<RectTransform>().rect.width;
        float x = _AnswersObj[3].GetComponent<RectTransform>().anchoredPosition.x;
        float y = _AnswersObj[3].GetComponent<RectTransform>().anchoredPosition.y;
        float width2 = _RightSignature.GetComponent<RectTransform>().rect.width;
        Vector3 pos = Vector3.zero;
        pos.x = x - width1 / 2 - width2 / 2;
        pos.y = y;
        _RightSignature.GetComponent<RectTransform>().anchoredPosition = pos;
        _RightSignature.SetActive(true);
    }

}
