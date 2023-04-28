using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject changeButtonObject;
    public Button changeButton;
    public string changeSceneName;
    
    void Start()
    {
        changeButton.onClick.AddListener(delegate(){
            this.ChangeToNextScene(changeSceneName);
        });
    }

    public void ChangeToNextScene(string changeSceneName)
    {
        Globe._NextSceneName = changeSceneName;
        SceneManager.LoadScene("Loading");
    }
}
