using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpeedAndTorque : MonoBehaviour
{
    public Vector2 ThrustRange;
    public Vector2 TorqueRange;

    public bool RandomDirection;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Go()
    {
        Vector3 dir = RandomDirection ? Random.insideUnitCircle.normalized : new Vector2(transform.up.x, transform.up.y);
        _rigidBody.AddForce(dir * Random.Range(ThrustRange.x, ThrustRange.y), ForceMode2D.Impulse);
        _rigidBody.AddTorque(Random.Range(TorqueRange.x, TorqueRange.y), ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        _rigidBody.angularVelocity = 0;
        _rigidBody.velocity = Vector3.zero;
    }
}
