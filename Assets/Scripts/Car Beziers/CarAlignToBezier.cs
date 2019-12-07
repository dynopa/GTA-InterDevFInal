using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAlignToBezier : MonoBehaviour
{

    [HideInInspector] public Vector3 dir;

    [Range(.01f, .25f)]
    public float tOffset;
    public float maxRotationSpeed;

    Vector3 currentFrame;
    Vector3 pastFrame;
    Vector3 currentVelocity;

    public CarFollowBezier followBezier;



    void Update()
    {

        //The function will look towards a point that is "tOffset" percent ahead of the current bezier
        if (followBezier.enRoute && followBezier.canRotate)
        {

            currentFrame = transform.position;
            currentVelocity = currentFrame - pastFrame;
            currentVelocity.Normalize();



            dir = currentVelocity;
            Quaternion directionVector = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, directionVector, Time.deltaTime * maxRotationSpeed);
        }

        pastFrame = transform.position;


    }
}
