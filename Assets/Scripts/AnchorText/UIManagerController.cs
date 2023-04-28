using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mime;
using UnityEngine.UI;

public class UIManagerController : MonoBehaviour
{
    public GameObject _ShowUpAnchorNamePrefab;
    public Canvas _Canvas;

    public GameObject _HurtImageObjectFather;
    public GameObject _HurtImagePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUpAnchorName(Vector2 uiPosition, string anchorName)
    {
        GameObject _AnchorNameObject = Instantiate(_ShowUpAnchorNamePrefab, uiPosition, Quaternion.identity);
        _AnchorNameObject.transform.SetParent(_Canvas.transform, false);
        ShowClickedAnchorName _ShowClickedAnchorName = _AnchorNameObject.GetComponent<ShowClickedAnchorName>();
        if (_ShowClickedAnchorName != null)
        {
            _ShowClickedAnchorName.ShowUp(anchorName, uiPosition);
        }
    }

    public void ShowUpHurtImage()
    {
        GameObject _HurtImageObject = Instantiate(_HurtImagePrefab, Vector3.zero, Quaternion.identity);
        _HurtImageObject.transform.SetParent(_HurtImageObjectFather.transform, false);
        RedScreenFinal redScreenFinal = _HurtImageObject.GetComponent<RedScreenFinal>();
        if(redScreenFinal != null)
        {
            redScreenFinal.ShowUp();
        }
    }
}
