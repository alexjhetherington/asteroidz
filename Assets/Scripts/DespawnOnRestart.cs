using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        MessageKit.addObserver(MessageIds.INIT_RESTART, Despawn);
    }

    // Update is called once per frame
    void OnDisable()
    {
        MessageKit.removeObserver(MessageIds.INIT_RESTART, Despawn);
    }

    void Despawn()
    {
        TrashMan.despawn(gameObject);
    }
}
