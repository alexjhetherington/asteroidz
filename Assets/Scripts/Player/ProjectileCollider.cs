using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    public MatchPositionAndRotation Matcher;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var asteroidCollider = collision.GetComponent<AsteroidCollider>();
        if (asteroidCollider != null )
        {
            asteroidCollider.Matcher.Target.gameObject.GetComponent<AsteroidDeath>().Die();
            Matcher.Target.gameObject.GetComponent<ProjectileDeath>().Die();
        }
    }
}
