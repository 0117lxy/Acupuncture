using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform theBack;
    public int maxAnchorNumber;
    public float precisionForClick;
    private GameObject[] backAnchor;
    private Vector2 clickPosition;

    LifeNumberChange lifeNumberChange;
    private bool isChangeLifeNumber;
    void Awake()
    {
        backAnchor = new GameObject[maxAnchorNumber];
        for(int i = 0; i < backAnchor.Length; ++i)
        {
            backAnchor[i] = theBack.GetChild(i).gameObject;
        }
    }
    void Start()
    {
        lifeNumberChange = GameObject.Find("LifeNumber").GetComponent<LifeNumberChange>();
        isChangeLifeNumber = true;
    }

    // Update is called once per frame
    void Update()
    {
        CursorClickPosition();
    }
    private void CursorClickPosition()
    {
        Vector2 position = new Vector2(0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition;
            position = Camera.main.ScreenToWorldPoint(position);
            IsAnchorClicked(position);
            Debug.Log("Mouse clicked at: " + position.ToString());
        }
    }
    private void IsAnchorClicked(Vector2 mouseClickPosition)
    {
        foreach(GameObject gameObject in backAnchor)
        {
            Vector2 anchorPosition = gameObject.GetComponent<Transform>().position;    
            Debug.Log("anchorPosion is : " + anchorPosition.ToString());
            if(GetDistanceOf2Position(anchorPosition, mouseClickPosition) <= precisionForClick)
            {
                gameObject.SetActive(false);
            }
            else
            {
                isChangeLifeNumber = false;
            }
        }

        ChangeLifeNumber();
    }
    private float GetDistanceOf2Position(Vector2 position1, Vector2 position2)
    {
        Debug.Log("The distance is : " + Vector2.Distance(position1, position2).ToString());
        return Vector2.Distance(position1, position2);
    }
    void ChangeLifeNumber()
    {
        if(lifeNumberChange.theHeartNumber!=0 && isChangeLifeNumber==false)
        {
            lifeNumberChange.theHeartNumber--;
            isChangeLifeNumber=true;
        }
    }
}
