using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFinder
{
    public static Camera FindMainCamera()
    {
        GameObject camera = GameObject.FindWithTag("MainCamera");
        if(camera != null)
        {
            return camera.GetComponent<Camera>();
        }
        return null;
    }
}
