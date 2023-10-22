using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocity : MonoBehaviour
{
    private Rigidbody ballRb;
    [SerializeField] private float maxVelocity=50f;

    [SerializeField] private float curVelocity = 50f;
    // Start is called before the first frame update
    void Start()
    {
        ballRb=GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curVelocity = ballRb.velocity.magnitude;
        if (ballRb.velocity.magnitude>maxVelocity)
        {
            Vector3 curVelocityDirection= ballRb.velocity.normalized;
            ballRb.velocity = Vector3.zero;
            ballRb.AddForce(curVelocityDirection * (maxVelocity),ForceMode.VelocityChange);
        }
    }
}
