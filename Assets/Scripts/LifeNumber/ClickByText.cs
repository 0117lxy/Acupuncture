using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickByText : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    int randomNumber;
    public GameObject TextObject;
    private void Awake()
    {
        Random.InitState(0);
    }
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            randomNumber = Random.Range(0, 10);
            ChangeTextRandomly();
        }
    }

    public void ChangeTextRandomly()
    {
        text.text = "’‚ «£∫"+ randomNumber.ToString();
    }
}
