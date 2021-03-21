using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    public float Score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerCollider = collision.GetComponent<PlayerCollider>();
        if (playerCollider != null)
        {
            var particles = TrashMan.spawn(explosionPrefab, transform.position).GetComponent<ParticleSystem>();
            particles.Clear();
            particles.Play();

            TrashMan.despawn(gameObject);

            MessageKit<TargetCollider>.post(MessageIds.TARGET_HIT, this);
        }
    }
}
