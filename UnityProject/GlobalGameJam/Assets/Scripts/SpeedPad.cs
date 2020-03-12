using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    [SerializeField] float _velocityIncrement = 1f;
    [SerializeField] float _speedCap = 10f;

    void OnCollisionStay(Collision collisionInfo)
    {
        Rigidbody rb = collisionInfo.collider.attachedRigidbody;
        //Vector3 velocity = rb.velocity + transform.forward * _velocityIncrement;
        rb.velocity = transform.forward * _velocityIncrement; ;
    }
}
