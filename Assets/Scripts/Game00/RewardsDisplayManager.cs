using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Reward
{
    public static bool[] _IsHaveReward;//是否拥有这个reward 
}

public class RewardsDisplayManager : MonoBehaviour
{
    public Button _OpenRewardsDisplayPanelButton;//打开奖励陈列面板

    public int _RewardsNum;//奖励的数量
    public Button[] _RewardButtons;//奖励
    public string[] _RewardDetails;//奖励的细节信息
    public Sprite[] _RewardImage;//设计的奖励的图片
    public Sprite _InitialRewardImage;//最初的奖励图片
    public GameObject _RewardsDisplayPanel;
    public Button _RewardsDisplayButton;//奖励展览界面的close button
    

    public GameObject _RewardDetailsPanel;//奖励细节面板
    public Image _RewardDetailsImage;
    public Text _RewardDetailsText;
    public Button _RewardDetailsClose;

    public class _RewardGroup
    {
        public int Index;
        public Sprite Image;// 奖励图片
        public string Info;// 奖励信息
        public Button RewardButton;//奖励Button

        public void Register(Action<int> clickEvent)
        {
            RewardButton.onClick.AddListener(() =>
            {
                clickEvent(Index);
            });
        }
    }

    public List<_RewardGroup> _RewardGroups;

    private void Awake()
    {
        _RewardGroups = new List<_RewardGroup>();
    }

    private void Start()
    {
        //这是总的奖励的个数，后面要更改
        Reward._IsHaveReward = new bool[9];
        for (int i = 0; i < Reward._IsHaveReward.Length; i++)
        {
            Reward._IsHaveReward[i] = false;
        }

        

        //每次打开奖励陈列都进行一次初始化
        _OpenRewardsDisplayPanelButton.onClick.AddListener(delegate ()
        {
            if(_RewardsDisplayPanel.activeSelf == false)
            {
                _RewardsDisplayPanel.SetActive(true);

                InitRewardGroup();

                foreach (var reward in _RewardGroups)
                {
                    reward.Register(OnRewardButtonClick);
                }
            }
        });

        _RewardDetailsClose.onClick.AddListener(delegate ()
        {
            if(_RewardDetailsPanel.activeSelf == true)
            {
                _RewardDetailsPanel.SetActive(false);
            }
        });

        _RewardsDisplayButton.onClick.AddListener(delegate ()
        {
            if (_RewardsDisplayPanel.activeSelf == true)
            {
                _RewardsDisplayPanel.SetActive(false);
            }
        });
    }

    private void Update()
    {
        //当奖励陈列打开的时候就要进行一次初始化
    }

    public void InitRewardGroup()
    {
        for (int i = 0; i < _RewardsNum; i++)
        {

            if (Reward._IsHaveReward[i] == true)
            {
                _RewardButtons[i].GetComponent<Image>().sprite = _RewardImage[i];

                int rewardIndex = i; // 必须用一个中间变量保存i的值
                var reward = new _RewardGroup();
                reward.Index = rewardIndex;
                reward.Image = _RewardButtons[i].GetComponent<Image>().sprite;
                reward.Info = _RewardDetails[i];
                reward.RewardButton = _RewardButtons[i];
                _RewardGroups.Add(reward);
            }
            else
            {
                _RewardButtons[i].GetComponent<Image>().sprite = _InitialRewardImage;
            }              
        }

        //不过这样写是不是浪费资源了，因为有时候它并不需要更新 date:23/5/2
        //这里所有的都要换图片，简写一下
        for (int i = 0; i < 9; i++)
        {
            _RewardButtons[i].GetComponent<Image>().sprite = _InitialRewardImage;
        }
    }

    private void OnRewardButtonClick(int rewardIndex)
    {
        // 显示奖励细节面板
        _RewardDetailsPanel.SetActive(true);

        // 设置奖励图片和信息文本
        _RewardDetailsImage.sprite = _RewardGroups[rewardIndex].Image;
        _RewardDetailsText.text = _RewardGroups[rewardIndex].Info;

    }

    public void OnCloseRewardDetailsPanelButtonClick()
    {
        // 隐藏奖励细节面板
        _RewardDetailsPanel.SetActive(false);
    }


}
