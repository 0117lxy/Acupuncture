using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipInfoController : MonoBehaviour
{
    //提示信息物体
    public Transform _TipInfo;
    public GameObject[] _Tips;
    public int _TipCount;
    public bool _IsShowAllTips = false;

    //用于获取bool值的物体
    public GameObject _NiddleType;//点击选择针的物体
    ClickToInstantiate _ClickToInstantiate;
    public GameObject _Anchor1_1;//第一组穴位的第一个穴位，用于新手指引
    Acupuncture _Acupuncture;

    //开始练习游戏的提示
    public GameObject _BeginTest;

    private void Awake()
    {
        _Tips = new GameObject[_TipCount];
    }

    private void Start()
    {
        //_NiddleType = GameObject.Find("NiddleType");
        _ClickToInstantiate = _NiddleType.GetComponent<ClickToInstantiate>();
        //_Anchor1_1 = GameObject.Find("Anchor1_1");
        _Acupuncture = _Anchor1_1.GetComponent<Acupuncture>();

        for (int i = 0; i < _TipCount; i++)
        {
            _Tips[i] = _TipInfo.GetChild(i).gameObject;
            _Tips[i].SetActive(false);
        }
    }

    private void Update()
    {
        ShowTips(_IsShowAllTips);

    }
    
    //显示新手提示
    void ShowTips(bool flag)
    {
        if(flag == false)
        {
            if (_ClickToInstantiate._IsSpawn == true)
            {
                if (_Tips[0].activeSelf == true)
                {
                    _Tips[0].SetActive(false);
                    _Tips[1].SetActive(true);
                }
            }
            if (_Acupuncture._State == Acupuncture.AcupunctureState.Focus)
            {
                if (_Tips[1].activeSelf == true)
                {
                    _Tips[1].SetActive(false);
                    _Tips[2].SetActive(true);
                }
            }
            if (_Acupuncture._State == Acupuncture.AcupunctureState.Strengthen)
            {
                if (_Tips[2].activeSelf == true)
                {
                    _Tips[2].SetActive(false);
                    _Tips[3].SetActive(true);
                }
            }
            if (_Acupuncture._State == Acupuncture.AcupunctureState.Niddling)
            {
                if (_Tips[3].activeSelf == true)
                {
                    _Tips[3].SetActive(false);
                    _IsShowAllTips = true;
                }
            }
        }
    }


}
