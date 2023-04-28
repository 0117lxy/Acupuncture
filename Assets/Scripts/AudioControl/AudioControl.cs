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
        // 初始化slider值为当前音乐音量大小
        volumeSlider.value = audioSource.volume;

        // 监听slider值改变事件
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }

    void OnVolumeChanged()
    {
        // 更新音乐音量大小为slider值
        audioSource.volume = volumeSlider.value;
    }
}
