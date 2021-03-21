using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private AsteroidManager _asteroidManager;
    private GameObject _playerInstance;

    void Start()
    {
        _asteroidManager = GetComponent<AsteroidManager>();
        _playerInstance = TrashMan.spawn(playerPrefab);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_playerInstance.activeInHierarchy)
        {
            TrashMan.spawn(playerPrefab);
            _asteroidManager.Restart();

            MessageKit.post(MessageIds.INIT_RESTART);
        }
    }
}
