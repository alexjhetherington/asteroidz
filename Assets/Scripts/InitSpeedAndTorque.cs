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

        //Cancel out intertia because it's calculated after a few frames for asteroids
        //I believe it's because I'm using compound colliders (colliders in children)
        //It means thrust values affect asteroids differently in the first few frames and it's annoying to deal with
        float torque = Random.Range(TorqueRange.x, TorqueRange.y);
        _rigidBody.AddTorque(torque * _rigidBody.inertia, ForceMode2D.Impulse); 
    }

    private void OnDisable()
    {
        _rigidBody.angularVelocity = 0;
        _rigidBody.velocity = Vector3.zero;
    }
}
