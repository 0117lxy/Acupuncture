using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameQuestion
{
    public static int _QuestionNum;//�����ǵڼ�������
}

public class QuestionManager : MonoBehaviour
{
    Questions _Questions;//����
    public Button _QuestionButton;//�����ϴ���������Button

    public GameObject _QuestionRolePanel;//�������д�����˵����
    public Button _QuestionRoleButton;//�������д�����˵�����Button

    public GameObject _QuestionPanel;//�������
    public GameObject _QuestionObj;//�������
    public GameObject[] _AnswersObj;//�����
    public Button _AnswerButton;//���������ȷ���𰸵�button
    public GameObject _RightAnswerText;//��ȷ�𰸵����
    public Button _CloseButton;//�ر���������Button

    public GameObject _TruePanel;//��ȷ���
    public Button _TrueButton;//��ȷ��ť

    public GameObject _WrongPanel;//�������
    public Button _WrongButton;//����ť

    public GameObject _RewardPanel;//�������
    public Button _RewardButton;//������ť

    private int currentQuestionIndex = 0; // ��ǰ��������
    private int selectedAnswerIndex = -1; // �û�ѡ��Ĵ�����

    public GameObject _RightSignature;//��ȷ��ͼƬ
    private float _OffsetX;//��ȷͼƬӦ���ڵ�λ��

    private void Start()
    {
        _Questions = GetComponent<Questions>();

        GameQuestion._QuestionNum = 0;

        //������������
        _QuestionButton.onClick.AddListener(delegate ()
        {
            if (_QuestionRolePanel.activeSelf == false)
            {
                _QuestionRolePanel.SetActive(true);
            }
        });

        //���������ϵͳ
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

        //���������ȷ���𰸵�button������ύ��
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

        //��ȷѡ���˴𰸵�panel��button����¼�
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

        //����ѡ���˴𰸵�panel��button����¼�
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

        //ѡ�������д𰸣���ý���������button
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
        //������������һ��
        currentQuestionIndex = GameQuestion._QuestionNum;

        _QuestionObj.GetComponent<Text>().text = _Questions.questions[currentQuestionIndex].questionName;

        for(int i = 0; i < _AnswersObj.Length; i++)
        {
            //_AnswersObj[i].GetComponent<Text>().text = (char)('A'+(char)i) + '.' + _Questions.questions[GameQuestion._QuestionNum].answers[i];
            //�ַ�����ֵ
            string answerText = $"{(char)('A' + i)}.{_Questions.questions[currentQuestionIndex].answers[i]}";
            _AnswersObj[i].GetComponent<Text>().text = answerText;
        }
    }

    //ѡ�����ĸ��𰸣�����ѡ���index
    public void AnswerSelected(int index)
    {
        selectedAnswerIndex = index;
    }

    //�ύ��
    public void SubmitAnswer()
    {

        //����Ѿ��ش������е�����
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
                    _RightAnswerText.GetComponent<Text>().text = "��ȷ�𰸣�" + $"{(char)('A' + _Questions.questions[currentQuestionIndex].correctAnswerIndex)}";
                    _RightAnswerText.SetActive(true);
                }
                //���ý������й��ж�Ӧλ�õĽ���Ϊtrue����ʾ�Ѿ���øý���
                Reward._IsHaveReward[1] = true;
            }
            else if(selectedAnswerIndex != _Questions.questions[currentQuestionIndex].correctAnswerIndex)
            {
                // �ش������ʾ�ͷ����
                if (_WrongPanel.activeSelf == false)
                {
                    _WrongPanel.SetActive(true);
                }
            }
            
        }

        else if (selectedAnswerIndex == _Questions.questions[currentQuestionIndex].correctAnswerIndex)
        {
            // �ش���ȷ����ʾ�������
            if(_TruePanel.activeSelf == false)
            {
                _TruePanel.SetActive(true);
            }
            if(_RightAnswerText.activeSelf == false)
            {
                _RightAnswerText.GetComponent<Text>().text = "��ȷ�𰸣�" + $"{(char)('A' + _Questions.questions[currentQuestionIndex].correctAnswerIndex)}";
                _RightAnswerText.SetActive(true);
            }

            
        }
        else
        {
            // �ش������ʾ�ͷ����
            if (_WrongPanel.activeSelf == false)
            {
                _WrongPanel.SetActive(true);
            }
        }

        // ����ѡ��Ĵ�����
        selectedAnswerIndex = -1;
    }

    //ѡ�����ĸ��𰸣���ʾ�Ժ�ͼƬ
    public void OnPointer1Down()
    {
        Debug.Log("ѡ���˵�һ����");
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
        Debug.Log("ѡ���˵ڶ�����");
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
        Debug.Log("ѡ���˵�������");
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
        Debug.Log("ѡ���˵��ĸ���");
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
