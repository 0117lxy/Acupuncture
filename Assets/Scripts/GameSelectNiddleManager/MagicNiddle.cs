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
    public GameObject[] _Bags;//���������������bag
    public GameObject[] _Niddles;//������

    private bool _GameStart = false;//��Ϸ�Ƿ��Ѿ���ʼ
    public bool _BagsMoving = false;//bag�Ƿ������ƶ�
    public bool _IsBagsCanMove;//bag�Ƿ�����ƶ�

    public Coroutine _CoroutineBagsMoveToBottom;

    public float _OffsetX;//bag�����niddle��x��ƫ��
    public float _OffsetY;//bag�����niddle��y��ƫ��

    public float _SpeedBottom;//bag�����ƶ����ٶ�
    public float _MoveToBottomDuration;//��ʱ����������ƶ����ٶ�
    public bool _IsMoveToBottom;//�Ƿ��Ѿ��ƶ�������

    public Coroutine _CoroutineSwag;//������Э��
    public float _SwagDuration;//����λ�õ�ʱ��
    public float _SwagGapDuration;//���ν����ļ��ʱ��
    private int _CurSwagNum;//�Ѿ������Ĵ���
    public int _SwagInvokeTime;//�����Ĵ���
    public bool _IsBagsCanSwag;//�Ƿ���Կ�ʼ����

    private void Start()
    {
        GameSelectNiddle._NiddleNum = 0;

        //��Ļ�м�λ��
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
        //��ʼ�����ƶ�bags
        if(_BagsMoving == false && _IsBagsCanMove == true)
        {
            if(_CoroutineBagsMoveToBottom == null)
            {
                _CoroutineBagsMoveToBottom = StartCoroutine(MoveToBottom());
            }
        }

        //���bags�����ƶ���Ͼͽ�����Э��
        if(_IsMoveToBottom == true)
        {
            DestroyMoveToBottom();
        }

        //��ʼ�������ڹ涨ʱ���ڽ���
        if (_BagsMoving == false && _IsBagsCanSwag == true && _CurSwagNum < _SwagInvokeTime)
        {
                           
            _CoroutineSwag = StartCoroutine(SwagBags());
            Debug.Log("����Э��SwagBags");
            //Invoke("DestroySwag", _SwagInvokeTime * _SwagDuration + (_SwagInvokeTime - 1) * _SwagGapDuration);
                        
        }

    }

    //bags�����ƶ�����סniddle�����м��Ǹ�bags��סniddle
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

    //����bags��˳�����Э�̿�ʼ��ʱ�򣬾�һֱ����bags��λ��
    IEnumerator SwagBags()
    {
        Debug.Log("SwagBagsЭ�̿�ʼ��");
        _BagsMoving = true;
        _IsBagsCanSwag = false;

        // �����������bag������
        int index1 = Random.Range(0, _Bags.Length);
        int index2 = Random.Range(0, _Bags.Length);
        if(index1 == index2)
        {
            index1 = 0;
            index2 = _Bags.Length - 1;
        }
        Debug.Log("index1 : " + index1);
        Debug.Log("index2 : " + index2);

        //ʱ��
        float t = 0;

        // ��������bag��λ��
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
        Debug.Log("���ڵ�ʱ��tΪ��" + t);
        Debug.Log("�����Ѿ�swag��" + _CurSwagNum + "��");
        
        // �ȴ�һ��ʱ��
        yield return new WaitForSeconds(_SwagGapDuration);

        _BagsMoving = false;
        _IsBagsCanSwag = true;

        yield break;
        Debug.Log("SwagBagsЭ��ִ�е�ĩβ��");
    }

    //���������ƶ�Э��
    public void DestroyMoveToBottom()
    {
        if(_CoroutineBagsMoveToBottom != null)
        {
            StopCoroutine(_CoroutineBagsMoveToBottom);
            _CoroutineBagsMoveToBottom = null;
            _BagsMoving = false;
        }
    }

    //���ٽ���Э��
    public void DestroySwag()
    {
        if(_CoroutineSwag != null)
        {
            Debug.Log("����Э��SwagBags");
            StopCoroutine(_CoroutineSwag);
            _CoroutineSwag = null;
            _BagsMoving = false;
            //_IsBagsCanSwag = false;
        }
    }


}
