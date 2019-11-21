using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAlignWithBezier : MonoBehaviour {
    
    [HideInInspector]public Vector3 dir;

    [Range(.01f, .25f)]
    public float tOffset;
    public float maxRotationSpeed;

    Vector3 currentFrame;
    Vector3 pastFrame;
    Vector3 currentVelocity;
    
    CarFollowBezier followBezier;

    Rigidbody rb;

    private void Start()
    {
        followBezier = GetComponent<CarFollowBezier>();
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate () {

        //The function will look towards a point that is "tOffset" percent ahead of the current bezier
        if (followBezier.enRoute && followBezier.canRotate)
        {

            currentFrame = rb.transform.position;
            currentVelocity = currentFrame - pastFrame;
            currentVelocity.Normalize();
            


            dir = currentVelocity;
            Quaternion directionVector = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, directionVector, Time.deltaTime * maxRotationSpeed);
        }

        pastFrame = rb.transform.position;
        

    }
}
