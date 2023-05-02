using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float _ShakeIntensity = 1f;//shakeǿ��
    public float _ShakeTime = 0.2f;//shakeʱ��

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    //��ʼ��
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
        //�����ʼ��shake��timerʱ����ֹͣshake
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                StopShake();
            }
        }
    }

    //��ʼshake�����ø÷�����ʼshake������ֵ���ٵ�ʱ��ֱ�ӵ��þͿ��ԡ�
    //����ֵ���ٵ�ʱ��ֱ�ӵ��û���̫�鷳��ֱ�ӷ��ڹ�������ֵ��manager�У���������ֵ�Զ����á�
    public void StartShake()
    {
        Debug.Log("Shake Camera!!!");
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = _ShakeIntensity;
        timer = _ShakeTime;
    }

    //ֹͣshake�ķ���
    void StopShake()
    {
        Debug.Log("Stop Shake Camera!!!");
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }


}
