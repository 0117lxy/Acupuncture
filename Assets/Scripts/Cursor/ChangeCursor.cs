using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursorPointer;
    public Texture2D cursorTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
    }
}
