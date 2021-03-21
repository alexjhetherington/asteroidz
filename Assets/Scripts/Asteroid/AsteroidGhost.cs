using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGhost : MonoBehaviour
{
    [SerializeField] private float ghostTime;

    private const string SHADER_DISSOLVE_PROP_NAME = "_Level";

    private SpriteRenderer _sprite;
    private CircleCollider2D _circleCollider;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetToSolid()
    {
        StopAllCoroutines();

        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetFloat(SHADER_DISSOLVE_PROP_NAME, 0);
        _sprite.SetPropertyBlock(props);

        _circleCollider.enabled = true;
    }

    public void SpawnAsGhost()
    {
        StartCoroutine(DoGhostTime());
    }

    private IEnumerator DoGhostTime()
    {
        _circleCollider.enabled = false;

        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetFloat(SHADER_DISSOLVE_PROP_NAME, 1);
        _sprite.SetPropertyBlock(props);

        float dissolve = 1f;
        while (dissolve > 0)
        {
            dissolve = Mathf.MoveTowards(dissolve, 0, Time.deltaTime / ghostTime);
            props.SetFloat(SHADER_DISSOLVE_PROP_NAME, dissolve);
            _sprite.SetPropertyBlock(props);
            yield return null;
        }

        _circleCollider.enabled = true;
    }
}
