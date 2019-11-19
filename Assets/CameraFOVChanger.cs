using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{

    public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public float start;
    public float end;

    public float minVelocity;
    public float maxVelocity;
    float t;
    // Use this for initialization
    void Start()
    {
        t = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("t " + t);
        //t += Time.deltaTime;
        ////float s = t / PlayerManager.Instance.GetComponent<Rigidbody>().velocity.z;
        //Debug.Log("s " + t);

        float velocity = PlayerManager.Instance.GetComponent<Rigidbody>().velocity.magnitude;
        
        velocity = velocity.Remap(minVelocity, maxVelocity, 0, 1);
        velocity = Mathf.Clamp(curve.Evaluate(velocity), 0, 1);
        velocity = velocity.Remap(0, 1, 70, 110);
        this.GetComponent<Camera>().fieldOfView = velocity;

    }
}
