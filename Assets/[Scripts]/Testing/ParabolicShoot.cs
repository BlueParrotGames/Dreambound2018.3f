﻿using System.Collections;
using UnityEngine;

public class ParabolicShoot : MonoBehaviour
{

    public Vector3 target;                                         /* The target object to hit */

    public float _time = 3f;                                                    /* The time of the travel */
    public Vector3 _startVelocity = new Vector3(0, 0, 0);                       /* The start velocity to be applied to the shot object before the shoot */
    public Vector3 speed = Vector3.zero;                                        /* Shows the speed during the shoot */
    public float _elapsed = 0f;                                                 /* Time elapsed from the starting of the shoot */

    Rigidbody rb;                                                               /* Rigidbody of the shot object */

    float _timeStartThrust = 0f;                                                /* Time the thrust force has been applied */
    bool shot;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();        // Rigidbody caching
    }

    void Update()
    {
        speed = rb.velocity;

        if(gameObject.name.Contains("Dev"))
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                Launch();
            }
        }
    }

    public void Launch()
    {
        Debug.Log(target);
        ApplyStartVelocity();
        ApplyThrust();
        shot = true;
    }

    public void ApplyStartVelocity()
    {
        rb.isKinematic = false;

        rb.AddForce(Vector3.right * _startVelocity.x, ForceMode.VelocityChange);
        rb.AddForce(Vector3.up * _startVelocity.y, ForceMode.VelocityChange);
        rb.AddForce(Vector3.forward * _startVelocity.z, ForceMode.VelocityChange);
    }

    public void ApplyThrust()
    {
        float X;
        float Y;
        float Z;
        float X0;
        float Y0;
        float Z0;
        float V0x;
        float V0y;
        float V0z;
        float t;

        rb.isKinematic = false;    // Avoid bouncing of the body before the shoot

        Vector3 forceDirection = target - transform.position;

        X = forceDirection.x;         // Distance to travel along X : Space traveled @ time t
        Y = forceDirection.y;         // Distance to travel along Y : Space traveled @ time t
        Z = forceDirection.z;         // Distance to travel along Z : Space traveled @ time t

        // As we calculate in this very moment the distance between the shot object and the target, the intial space coordinates X0, Y0, Z0 will be always 0.
        X0 = 0;
        Y0 = 0;
        Z0 = 0;

        t = _time;

        // Calculation of the required velocity along each axis to hit the target from the current starting position as if the shot object were stopped 
        V0x = (X - X0) / t;
        V0z = (Z - Z0) / t;
        V0y = (Y - Y0 + (0.4f * Mathf.Abs(Physics.gravity.magnitude) * Mathf.Pow(t, 2))) / t;

        /* Subtraction of the current velocity of the shot object */
        V0x -= rb.velocity.x;
        V0y -= rb.velocity.y;
        V0z -= rb.velocity.z;

        rb.AddForce(Vector3.right * V0x * 1.2f, ForceMode.VelocityChange);    // VelocityChange Add an instant velocity change to the rigidbody, applying an impulsive force, ignoring its mass.
        rb.AddForce(Vector3.up * V0y, ForceMode.VelocityChange);
        rb.AddForce(Vector3.forward * V0z * 1.2f, ForceMode.VelocityChange);

        _timeStartThrust = Time.time;
    }

    void OnCollisionEnter(Collision c)
    {
        if(shot && c.gameObject.CompareTag("Floor"))
        {
            rb.drag = 1;
        }
    }
}