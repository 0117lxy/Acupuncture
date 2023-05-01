using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectNiddle
{
    public static int _NiddleNum;
}

public class MagicNiddle : MonoBehaviour
{
    public GameObject[] _Bags;//三个用于隐藏针的bag
    public GameObject[] _Niddles;//三个针

    private bool _GameStart = false;//游戏是否已经开始
    public bool _BagsMoving = false;//bag是否正在移动
    public bool _IsBagsCanMove;//bag是否可以移动

    public Coroutine _CoroutineBagsMoveToBottom;

    public float _OffsetX;//bag相对于niddle的x轴偏移
    public float _OffsetY;//bag相对于niddle的y轴偏移

    public float _SpeedBottom;//bag向下移动的速度
    public float _MoveToBottomDuration;//以时间控制向下移动的速度
    public bool _IsMoveToBottom;//是否已经移动到下面

    public Coroutine _CoroutineSwag;//交换的协程
    public float _SwagDuration;//交换位置的时间
    public float _SwagGapDuration;//两次交换的间隔时间
    private int _CurSwagNum;//已经交换的次数
    public int _SwagInvokeTime;//交换的次数
    public bool _IsBagsCanSwag;//是否可以开始交换

    private void Start()
    {
        GameSelectNiddle._NiddleNum = 0;

        //屏幕中间位置
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        for(int i = 0; i < _Bags.Length; i++)
        {
            Vector3 pos = new Vector3(-(_Bags.Length - 1) * _OffsetX + i * 2 * _OffsetX,
                                      _OffsetY,
                                      0);
            _Bags[i].GetComponent<RectTransform>().anchoredPosition = pos;
        }

    }

    private void Update()
    {
        //开始向下移动bags
        if(_BagsMoving == false && _IsBagsCanMove == true)
        {
            if(_CoroutineBagsMoveToBottom == null)
            {
                _CoroutineBagsMoveToBottom = StartCoroutine(MoveToBottom());
            }
        }

        //如果bags向下移动完毕就结束该协程
        if(_IsMoveToBottom == true)
        {
            DestroyMoveToBottom();
        }

        //开始交换，在规定时间内结束
        if (_BagsMoving == false && _IsBagsCanSwag == true && _CurSwagNum < _SwagInvokeTime)
        {
                           
            _CoroutineSwag = StartCoroutine(SwagBags());
            Debug.Log("启动协程SwagBags");
            //Invoke("DestroySwag", _SwagInvokeTime * _SwagDuration + (_SwagInvokeTime - 1) * _SwagGapDuration);
                        
        }

    }

    //bags向下移动，盖住niddle，最中间那个bags盖住niddle
    IEnumerator MoveToBottom()
    {
        _BagsMoving = true;
        _IsBagsCanMove = false;

        /*while (_Bags[0].GetComponent<RectTransform>().anchoredPosition.y > 0)
        {
            for (int i = 0; i < _Bags.Length; i++)
            {
                if (_Bags[i].GetComponent<RectTransform>().anchoredPosition.y > 0)
                {
                    float y = _Bags[i].GetComponent<RectTransform>().anchoredPosition.y;
                    y -= Time.deltaTime * _SpeedBottom;
                    y = (y > 0) ? y : 0;
                    _Bags[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(
                        _Bags[i].GetComponent<RectTransform>().anchoredPosition.x,
                        y,
                        0); 
                }
            }

            yield return null;
        }*/

        float t = 0;
        Vector3[] startPos = new Vector3[_Bags.Length];
        for (int i = 0; i < _Bags.Length; i++)
        {
            startPos[i] = _Bags[i].GetComponent<RectTransform>().anchoredPosition;
        }

        while (t < _MoveToBottomDuration)
        {
            t += Time.deltaTime;

            for (int i = 0; i < _Bags.Length; i++)
            {
                if (_Bags[i].GetComponent<RectTransform>().anchoredPosition.y > 0)
                {
                    Vector3 pos = _Bags[i].GetComponent<RectTransform>().anchoredPosition;

                    float y = Mathf.SmoothStep(startPos[i].y, 0, t / _MoveToBottomDuration);

                    pos.y = y;
                    _Bags[i].GetComponent<RectTransform>().anchoredPosition = pos;
                }
            }

            yield return null;
        }

        _IsMoveToBottom = true;

    }

    //交换bags的顺序，这个协程开始的时候，就一直交换bags的位置
    IEnumerator SwagBags()
    {
        Debug.Log("SwagBags协程开始了");
        _BagsMoving = true;
        _IsBagsCanSwag = false;

        // 随机生成两个bag的索引
        int index1 = Random.Range(0, _Bags.Length);
        int index2 = Random.Range(0, _Bags.Length);
        if(index1 == index2)
        {
            index1 = 0;
            index2 = _Bags.Length - 1;
        }
        Debug.Log("index1 : " + index1);
        Debug.Log("index2 : " + index2);

        //时间
        float t = 0;

        // 交换两个bag的位置
        Vector3 startPos1 = _Bags[index1].GetComponent<RectTransform>().anchoredPosition;
        Vector3 startPos2 = _Bags[index2].GetComponent<RectTransform>().anchoredPosition;
        Vector3 endPos1 = _Bags[index2].GetComponent<RectTransform>().anchoredPosition;
        Vector3 endPos2 = _Bags[index1].GetComponent<RectTransform>().anchoredPosition;

        while (t < _SwagDuration)
        {
            t += Time.deltaTime;

            if(_Bags[index1].GetComponent<RectTransform>().anchoredPosition.x != endPos1.x)
            {
                Vector3 pos = _Bags[index1].GetComponent<RectTransform>().anchoredPosition;
                float x = Mathf.SmoothStep(startPos1.x, endPos1.x, t / _SwagDuration);
                pos.x = x;
                _Bags[index1].GetComponent<RectTransform>().anchoredPosition = pos;
                if(index1 == _Bags.Length / 2)
                {
                    _Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition = pos;
                }
            }
            if (_Bags[index2].GetComponent<RectTransform>().anchoredPosition.x != endPos2.x)
            {
                Vector3 pos = _Bags[index2].GetComponent<RectTransform>().anchoredPosition;
                float x = Mathf.SmoothStep(startPos2.x, endPos2.x, t / _SwagDuration);
                pos.x = x;
                _Bags[index2].GetComponent<RectTransform>().anchoredPosition = pos;
                if (index2 == _Bags.Length / 2)
                {
                    _Niddles[GameSelectNiddle._NiddleNum].GetComponent<RectTransform>().anchoredPosition = pos;
                }
            }

            yield return null;
        }

        _CurSwagNum++;
        Debug.Log("现在的时间t为：" + t);
        Debug.Log("现在已经swag：" + _CurSwagNum + "次");
        
        // 等待一段时间
        yield return new WaitForSeconds(_SwagGapDuration);

        _BagsMoving = false;
        _IsBagsCanSwag = true;

        yield break;
        Debug.Log("SwagBags协程执行到末尾了");
    }

    //销毁向下移动协程
    public void DestroyMoveToBottom()
    {
        if(_CoroutineBagsMoveToBottom != null)
        {
            StopCoroutine(_CoroutineBagsMoveToBottom);
            _CoroutineBagsMoveToBottom = null;
            _BagsMoving = false;
        }
    }

    //销毁交换协程
    public void DestroySwag()
    {
        if(_CoroutineSwag != null)
        {
            Debug.Log("结束协程SwagBags");
            StopCoroutine(_CoroutineSwag);
            _CoroutineSwag = null;
            _BagsMoving = false;
            //_IsBagsCanSwag = false;
        }
    }


}
