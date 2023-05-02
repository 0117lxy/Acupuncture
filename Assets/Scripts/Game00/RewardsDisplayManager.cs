using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Reward
{
    public static bool[] _IsHaveReward;//�Ƿ�ӵ�����reward 
}

public class RewardsDisplayManager : MonoBehaviour
{
    public Button _OpenRewardsDisplayPanelButton;//�򿪽����������

    public int _RewardsNum;//����������
    public Button[] _RewardButtons;//����
    public string[] _RewardDetails;//������ϸ����Ϣ
    public Sprite[] _RewardImage;//��ƵĽ�����ͼƬ
    public Sprite _InitialRewardImage;//����Ľ���ͼƬ
    public GameObject _RewardsDisplayPanel;
    public Button _RewardsDisplayButton;//����չ�������close button
    

    public GameObject _RewardDetailsPanel;//����ϸ�����
    public Image _RewardDetailsImage;
    public Text _RewardDetailsText;
    public Button _RewardDetailsClose;

    public class _RewardGroup
    {
        public int Index;
        public Sprite Image;// ����ͼƬ
        public string Info;// ������Ϣ
        public Button RewardButton;//����Button

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
        //�����ܵĽ����ĸ���������Ҫ����
        Reward._IsHaveReward = new bool[9];
        for (int i = 0; i < Reward._IsHaveReward.Length; i++)
        {
            Reward._IsHaveReward[i] = false;
        }

        

        //ÿ�δ򿪽������ж�����һ�γ�ʼ��
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
        //���������д򿪵�ʱ���Ҫ����һ�γ�ʼ��
    }

    public void InitRewardGroup()
    {
        for (int i = 0; i < _RewardsNum; i++)
        {

            if (Reward._IsHaveReward[i] == true)
            {
                _RewardButtons[i].GetComponent<Image>().sprite = _RewardImage[i];

                int rewardIndex = i; // ������һ���м��������i��ֵ
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

        //��������д�ǲ����˷���Դ�ˣ���Ϊ��ʱ����������Ҫ���� date:23/5/2
        //�������еĶ�Ҫ��ͼƬ����дһ��
        for (int i = 0; i < 9; i++)
        {
            _RewardButtons[i].GetComponent<Image>().sprite = _InitialRewardImage;
        }
    }

    private void OnRewardButtonClick(int rewardIndex)
    {
        // ��ʾ����ϸ�����
        _RewardDetailsPanel.SetActive(true);

        // ���ý���ͼƬ����Ϣ�ı�
        _RewardDetailsImage.sprite = _RewardGroups[rewardIndex].Image;
        _RewardDetailsText.text = _RewardGroups[rewardIndex].Info;

    }

    public void OnCloseRewardDetailsPanelButtonClick()
    {
        // ���ؽ���ϸ�����
        _RewardDetailsPanel.SetActive(false);
    }


}
