using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float _ShakeIntensity = 1f;//shake强度
    public float _ShakeTime = 0.2f;//shake时间

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    //初始化
    void Awake()
    {
        cinemachineVirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        StopShake();
    }

    void Update()
    {
        //如果开始了shake，timer时间后就停止shake
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                StopShake();
            }
        }
    }

    //开始shake，调用该方法开始shake，生命值减少的时候直接调用就可以。
    //生命值减少的时候直接调用还是太麻烦，直接放在管理生命值的manager中，减少生命值自动调用。
    public void StartShake()
    {
        Debug.Log("Shake Camera!!!");
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = _ShakeIntensity;
        timer = _ShakeTime;
    }

    //停止shake的方法
    void StopShake()
    {
        Debug.Log("Stop Shake Camera!!!");
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }


}
