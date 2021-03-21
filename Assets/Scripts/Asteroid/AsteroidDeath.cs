using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDeath : MonoBehaviour
{
    [SerializeField] private GameObject asteroidExplosionPrefab;

    private AsteroidChildController asteroidChildController;

    void Start()
    {
        asteroidChildController = GetComponent<AsteroidChildController>();
    }

    public void Die()
    {
        TrashMan.despawn(gameObject);

        AsteroidKillInfo killInfo = new AsteroidKillInfo();
        killInfo.asteroid = gameObject;
        killInfo.scale = asteroidChildController.CurrentScale;
        MessageKit<AsteroidKillInfo>.post(MessageIds.ASTEROID_KILLED, killInfo);
        
        var particles = TrashMan.spawn(asteroidExplosionPrefab, transform.position).GetComponent<ParticleSystem>();
        particles.Clear();
        particles.Play();
    }
}
