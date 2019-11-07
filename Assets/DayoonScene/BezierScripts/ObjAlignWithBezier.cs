using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjAlignWithBezier : MonoBehaviour {
    
    [HideInInspector]public Vector3 dir;

    [Range(.01f, .25f)]
    public float tOffset;
    public float maxRotationSpeed;
    Vector3 currentFrame;
    Vector3 futureFrame;
    Vector3 currentVelocity;
    
    ObjFollowBezier followBezier;

    private void Start()
    {
        followBezier = GetComponent<ObjFollowBezier>();
    }


    void Update () {

        //The function will look towards a point that is "tOffset" percent ahead of the current bezier
        if (followBezier.enRoute)
        {
            futureFrame = followBezier.PositionAhead(tOffset);
            currentVelocity = futureFrame - currentFrame;
            currentVelocity.Normalize();
            currentFrame = transform.position;
        }
        
        
        dir = currentVelocity; 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * maxRotationSpeed);

    }
}
