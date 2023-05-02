using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameMove : MonoBehaviour
{
    public Vector2 _TargetPos;
    private Vector2 _StartPos;
    public float _Duration;

    private void Start()
    {
        _StartPos = this.GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(Move(_StartPos, _TargetPos));
    }

    IEnumerator Move(Vector2 startPos , Vector2 targetPos)
    {
        float timeMove = 0;   
        while(timeMove < _Duration)
        {
            Vector2 newPos = Vector2.Lerp(startPos, targetPos, timeMove / _Duration);
            GetComponent<RectTransform>().anchoredPosition = newPos;

            timeMove += Time.deltaTime;
            yield return null;
        }
    }
}
