using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float torque;

    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidBody.AddForce(thrust * transform.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddTorque(torque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rigidBody.AddTorque(torque * -1);
        }
    }
}
