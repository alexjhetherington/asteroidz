using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeToUnits : MonoBehaviour
{
    private Camera _mainCamera;
    
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    //TODO don't do this every frame update ;)
    //Do it only when screen size changed
    void Update()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        MessageKit<Vector2>.post(MessageIds.SCREEN_SIZE, new Vector2(width, height));
    }
}
