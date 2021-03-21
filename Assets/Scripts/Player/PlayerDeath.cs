using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    public void Die()
    {
        TrashMan.despawn(gameObject);

        var particles = TrashMan.spawn(explosionPrefab, transform.position).GetComponent<ParticleSystem>();
        particles.Clear();
        particles.Play();

        MessageKit.post(MessageIds.PLAYER_KILLED);
    }
}
