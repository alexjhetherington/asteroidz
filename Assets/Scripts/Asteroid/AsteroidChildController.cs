using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidChildController : MonoBehaviour
{
    [SerializeField] private AsteroidGhost[] children;

    public float CurrentScale { get; private set; } = 2;

    public void SetScale(float scale)
    {
        CurrentScale = scale;
        for (int i = 0; i < 9; i++)
        {
            children[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void SpawnAsSolid()
    {
        for (int i = 0; i < 9; i++)
        {
            children[i].SetToSolid();
        }
    }

    public void SpawnAsGhost()
    {
        for (int i = 0; i < 9; i++)
        {
            children[i].SpawnAsGhost();
        }
    }
}
