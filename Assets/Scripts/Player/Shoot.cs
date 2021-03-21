using Prime31.MessageKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;

    [SerializeField] private float ammoCost;
    [SerializeField] private float ammoRegen;

    private float _lastShotTime;

    private float _ammo = 1;

    void Start()
    {
        MessageKit.addObserver(MessageIds.PLAYER_KILLED, HandlePlayerKilled);
    }

    private void OnDestroy()
    {
        MessageKit.removeObserver(MessageIds.PLAYER_KILLED, HandlePlayerKilled);
    }

    // Update is called once per frame
    void Update()
    {
        _ammo = Mathf.Clamp01(_ammo + ammoRegen * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && Time.time > _lastShotTime + cooldown && _ammo > ammoCost)
        {
            _ammo -= ammoCost;
            _lastShotTime = Time.time;
            InitSpeedAndTorque speedAndTorque = TrashMan.spawn(projectile, transform.position, transform.rotation).GetComponent<InitSpeedAndTorque>();
            speedAndTorque.Go();
        }

        MessageKit<float>.post(MessageIds.AMMO_LEFT, _ammo);
    }

    private void HandlePlayerKilled()
    {
        _ammo = 1;
    }
}
