/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class VerticalTMP : MonoBehaviour
{
    public float delay = 0.1f; // �����ٶȣ������е���
    public string fullText; // �������ı�����

    private string currentText = ""; // ��ǰ�Ѿ���ʾ���ı�����
    private int visibleCharacterCount = 0; // �ɼ��ַ���
    private float timer = 0.0f; // ��ʱ��

    private TMP_Text textComponent; // TextMeshProUGUI ���

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>(); // ��ȡ TextMeshProUGUI ���
        textComponent.text = "<rotate=90>"; // �� TextMeshProUGUI ���ı��������
    }

    private void Update()
    {
        if (visibleCharacterCount < fullText.Length) // �����ǰ�ı�û����ȫ��ʾ���
        {
            timer += Time.deltaTime; // ��ʱ����ʼ��ʱ

            if (timer >= delay) // �����ʱ�������˴����ٶ�
            {
                visibleCharacterCount++; // ���ӿɼ��ַ���
                currentText = fullText.Substring(0, visibleCharacterCount); // ��ȡ��ǰ�ɼ����ı�����

                textComponent.text = StripRichTextTags(currentText); // ���� TextMeshProUGUI ���ı����ݣ�ȥ�����ı���ǩ��

                timer = 0.0f; // ��ʱ������
            }
        }
    }

    // ȥ�����ı���ǩ
    private string StripRichTextTags(string text)
    {
        TMP_TextInfo textInfo = textComponent.textInfo;

        int characterCount = 0;
        StringBuilder stringBuilder = new StringBuilder(text.Length);

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c == '<')
            {
                int closeTagIndex = text.IndexOf('>', i);
                i = closeTagIndex;
                continue;
            }

            if (characterCount < textInfo.characterCount)
            {
                if (textInfo.characterInfo[characterCount].isVisible)
                {
                    stringBuilder.Append(c);
                }

                characterCount++;
            }
        }

        return stringBuilder.ToString();
    }
}*/