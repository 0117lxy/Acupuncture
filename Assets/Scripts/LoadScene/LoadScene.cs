using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Globe
{
    public static string _NextSceneName;
}

public class LoadScene : MonoBehaviour
{
    public Image _ProgressBar;
    private int _CurrentProgressValue = 0;
    private AsyncOperation _AsyncOperation;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //����Э��
            StartCoroutine(AsyncLoading());
        }
    }

    void Update()
    {
        int progressValue = 100;

        if (_CurrentProgressValue < progressValue)
        {
            _CurrentProgressValue++;
        }

        _ProgressBar.fillAmount = _CurrentProgressValue / 100f;

        if (_CurrentProgressValue == 100)
        {
            _AsyncOperation.allowSceneActivation = true;
        }
    }

    IEnumerator AsyncLoading()
    {
        _AsyncOperation = SceneManager.LoadSceneAsync(Globe._NextSceneName);
        _AsyncOperation.allowSceneActivation = false;//��ֹ����������Զ��л�
        yield return _AsyncOperation;
    }

}


