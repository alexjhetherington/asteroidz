using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPositionAndRotation : MonoBehaviour
{
    public Transform Target;
    public Vector2 OffsetDirection;

    private Vector2 _offset;

    void Start()
    {
        MessageKit<Vector2>.addObserver(MessageIds.SCREEN_SIZE, UpdateOffsetBasedOnScreenSize);
    }

    void OnDestroy()
    {
        MessageKit<Vector2>.removeObserver(MessageIds.SCREEN_SIZE, UpdateOffsetBasedOnScreenSize);
    }

    void UpdateOffsetBasedOnScreenSize(Vector2 screenSize)
    {
        _offset = new Vector2(screenSize.x * OffsetDirection.x, screenSize.y * OffsetDirection.y);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Target.localPosition + new Vector3(_offset.x, _offset.y, 0);
        transform.rotation = Target.localRotation;
    }
}
