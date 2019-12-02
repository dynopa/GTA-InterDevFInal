using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera FOV Changer. Changes the FOV of the camera along the animation curve based on the velocity of the player.
/// </summary>
public class CameraFOVChanger : MonoBehaviour
{

    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    public float minVelocity;
    public float maxVelocity;


    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = PlayerManager.Instance.GetComponent<Rigidbody>().velocity.magnitude;
        //Debug.Log(velocity);
        //convert the velocity to a value along the animation curve, then convert that to a FOV value
        velocity = velocity.Remap(minVelocity, maxVelocity, 0, 1);
        velocity = Mathf.Clamp(curve.Evaluate(velocity), 0, 1);
        velocity = velocity.Remap(0, 1, 70, 100);
        this.GetComponent<Camera>().fieldOfView = velocity;

    }
}
