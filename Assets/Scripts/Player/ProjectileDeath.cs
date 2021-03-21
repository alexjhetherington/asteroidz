using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeath : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private float _enableTime;

    private void OnEnable()
    {
        _enableTime = Time.time;
    }

    public void Die()
    {
        TrashMan.despawn(gameObject);
    }

    private void Update()
    {
        if(Time.time > lifeTime + _enableTime)
        {
            Die();
        }
    }
}
