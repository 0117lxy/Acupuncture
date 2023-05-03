using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ChooseLevel : MonoBehaviour
{
    public Button[] _ReelLevelBtn;//打开卷轴的Btn
    public GameObject[] _ReelObj;//Reel的面板
    public Button[] _ReelCloseBtn;//关闭Reel的按钮
    public GameObject _Panel;//用于做背景黑化的Panel，这样设置比较方便

    public Button[] _LevelsBtn;//卷轴对应的进入关卡的btn
    public string[] _LevelsName;
    public int _LevelNum;

    public class _ReelLevelGroup
    {
        public GameObject _ReelObj;
        public Button _ReelLevelBtn;

        public void Register(Action<GameObject> chooseEvent)
        {
            _ReelLevelBtn.onClick.AddListener(() =>
            {
                chooseEvent(_ReelObj);
            });
        }
    }

    public class _LevelGroup
    {
        public string _NextScene;
        public Button _LevelsBtn;

        public void Register(Action<string> chooseEvent)
        {
            _LevelsBtn.onClick.AddListener(() =>
            {
                chooseEvent(_NextScene);
            });
        }
    }
    
    public List<_LevelGroup> _LevelGroups;
    public List<_ReelLevelGroup> _ReelLevelGroups;
    private void Awake()
    {
        /*_LevelsBtn = new Button[_LevelNum];
        _LevelsName = new string[_LevelNum];*/

        _LevelGroups = new List<_LevelGroup>();
        _ReelLevelGroups = new List<_ReelLevelGroup>();
    }
    private void Start()
    {
        InitLevelGroup();

        foreach (var level in _LevelGroups)
        {
            level.Register(ChooseTheLevel);
        }

        InitReelLevelGroup();
        foreach (var reelLevel in _ReelLevelGroups)
        {
            reelLevel.Register(ChooseTheReelLevel);
        }

        HideReelPanel();

        RegistCloseReelPanel();

    }

    void ChooseTheLevel(string _NextSccene)
    {
        Globe._NextSceneName = _NextSccene;
        SceneManager.LoadScene("Loading");
    }

    void InitLevelGroup()
    {
        for(int i = 0; i < _LevelNum; i++)
        {
            var level = new _LevelGroup();
            level._NextScene = _LevelsName[i];
            level._LevelsBtn = _LevelsBtn[i];
            _LevelGroups.Add(level);
        }
    }

    void ChooseTheReelLevel(GameObject obj)
    {
        if(obj.activeSelf == false)
        {
            obj.SetActive(true);
        }
        if (_Panel.activeSelf == false)
        {
            _Panel.SetActive(true);
        }
    }

    void InitReelLevelGroup()
    {
        for (int i = 0; i < _LevelNum; i++)
        {
            var reelLevel = new _ReelLevelGroup();
            reelLevel._ReelObj = _ReelObj[i];
            reelLevel._ReelLevelBtn = _ReelLevelBtn[i];
            _ReelLevelGroups.Add(reelLevel);
        }
    }

    void HideReelPanel()
    {
        for(int i = 0; i < _ReelObj.Length ; i++)
        {
            if(_ReelObj[i].activeSelf == true)
            {
                _ReelObj[i].SetActive(false);
            }
        }

        if (_Panel.activeSelf == true)
        {
            _Panel.SetActive(false);
        }
    }

    void RegistCloseReelPanel()
    {
        for(int i = 0; i < _ReelCloseBtn.Length; i++)
        {
            _ReelCloseBtn[i].onClick.AddListener(delegate ()
            {
                HideReelPanel();
            });
        }
    }
}
