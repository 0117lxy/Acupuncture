using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ChooseLevel : MonoBehaviour
{

    public Button[] _LevelsBtn;
    public string[] _LevelsName;
    public int _LevelNum;

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

    private void Awake()
    {
        /*_LevelsBtn = new Button[_LevelNum];
        _LevelsName = new string[_LevelNum];*/

        _LevelGroups = new List<_LevelGroup>();
    }
    private void Start()
    {
        InitLevelGroup();

        foreach (var level in _LevelGroups)
        {
            level.Register(ChooseTheLevel);
        }
    }

    void ChooseTheLevel(string _NextSccene)
    {
        Globe._NextSceneName = _NextSccene;
        SceneManager.LoadScene("Loading");
    }

    void InitLevelGroup()
    {
        for(int i=0; i < _LevelNum; i++)
        {
            var level = new _LevelGroup();
            level._NextScene = _LevelsName[i];
            level._LevelsBtn = _LevelsBtn[i];
            _LevelGroups.Add(level);
        }
    }
}
