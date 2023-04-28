using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        // ��ʼ��sliderֵΪ��ǰ����������С
        volumeSlider.value = audioSource.volume;

        // ����sliderֵ�ı��¼�
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }

    void OnVolumeChanged()
    {
        // ��������������СΪsliderֵ
        audioSource.volume = volumeSlider.value;
    }
}
