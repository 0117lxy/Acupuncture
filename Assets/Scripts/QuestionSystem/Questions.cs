using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionName;
    public string[] answers = new string[4];
    public int correctAnswerIndex;
}
public class Questions : MonoBehaviour
{
    public Question[] questions;
}
