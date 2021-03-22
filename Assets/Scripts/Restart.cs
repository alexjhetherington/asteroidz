using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    private GameObject _playerInstance;

    void Start()
    {
        _playerInstance = TrashMan.spawn(playerPrefab);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_playerInstance.activeInHierarchy)
        {
            TrashMan.spawn(playerPrefab);

            MessageKit.post(MessageIds.INIT_RESTART);
        }
    }
}
