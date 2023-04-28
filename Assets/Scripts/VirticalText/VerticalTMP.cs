/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class VerticalTMP : MonoBehaviour
{
    public float delay = 0.1f; // 打字速度，可自行调整
    public string fullText; // 完整的文本内容

    private string currentText = ""; // 当前已经显示的文本内容
    private int visibleCharacterCount = 0; // 可见字符数
    private float timer = 0.0f; // 计时器

    private TMP_Text textComponent; // TextMeshProUGUI 组件

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>(); // 获取 TextMeshProUGUI 组件
        textComponent.text = "<rotate=90>"; // 将 TextMeshProUGUI 的文本内容清空
    }

    private void Update()
    {
        if (visibleCharacterCount < fullText.Length) // 如果当前文本没有完全显示完毕
        {
            timer += Time.deltaTime; // 计时器开始计时

            if (timer >= delay) // 如果计时器超过了打字速度
            {
                visibleCharacterCount++; // 增加可见字符数
                currentText = fullText.Substring(0, visibleCharacterCount); // 获取当前可见的文本内容

                textComponent.text = StripRichTextTags(currentText); // 更新 TextMeshProUGUI 的文本内容（去掉富文本标签）

                timer = 0.0f; // 计时器清零
            }
        }
    }

    // 去掉富文本标签
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