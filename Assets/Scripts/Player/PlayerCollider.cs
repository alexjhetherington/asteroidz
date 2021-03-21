using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public MatchPositionAndRotation Matcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var asteroidCollider = collision.GetComponent<AsteroidCollider>();
        if (asteroidCollider != null)
        {
            Matcher.Target.gameObject.GetComponent<PlayerDeath>().Die();
        }
    }
}
