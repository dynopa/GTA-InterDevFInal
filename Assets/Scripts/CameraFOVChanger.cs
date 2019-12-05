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
    [Space]
    public float minY;
    public float maxY;
    [Space]
    [Range(0, 1)]
    public float changeYDamping;
    

    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Gets the velocity value and remaps it along the animation curve
        float velocity = PlayerManager.Instance.GetComponent<Rigidbody>().velocity.magnitude;

        velocity = velocity.Remap(minVelocity, maxVelocity, 0, 1);
        velocity = Mathf.Clamp(curve.Evaluate(velocity), 0, 1);
        float targetY = velocity.Remap(0, 1, minY, maxY);

        Vector3 movePos = new Vector3(Camera.main.transform.position.x, targetY, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, movePos, changeYDamping);

    }



}
