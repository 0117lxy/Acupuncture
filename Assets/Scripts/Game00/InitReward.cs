using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitReward : MonoBehaviour
{

    public int _RewardLength;

    void Start()
    {
        //�����ܵĽ����ĸ���������Ҫ����
        Reward._IsHaveReward = new bool[_RewardLength];

        for (int i = 0; i < Reward._IsHaveReward.Length; i++)
        {
            Reward._IsHaveReward[i] = false;
        }
    }

}