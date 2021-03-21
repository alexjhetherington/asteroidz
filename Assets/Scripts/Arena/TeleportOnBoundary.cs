using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnBoundary : MonoBehaviour
{
    private Vector2 _screenSize;

    void Start()
    {
        MessageKit<Vector2>.addObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
    }

    void OnDestroy()
    {
        MessageKit<Vector2>.removeObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
    }

    private void UpdateScreenSize(Vector2 value)
    {
        _screenSize = value;
    }

    //If Update is called before UpdateScreenSize (drive by Update on ScreenSizeToUnits) then teleport may happen too early / late
    //Can be fixed by setting ScreenSizeToUnits Update order to be early
    void Update()
    {
        Vector3 newPos = transform.localPosition;

        if (Mathf.Abs(transform.localPosition.x) > _screenSize.x / 2)
        {
            newPos.x = transform.localPosition.x - Mathf.Sign(transform.localPosition.x) * _screenSize.x;
        }

        if (Mathf.Abs(transform.localPosition.y) > _screenSize.y / 2)
        {
            newPos.y = transform.localPosition.y - Mathf.Sign(transform.localPosition.y) * _screenSize.y;
        }

        transform.localPosition = newPos;
    }
}
