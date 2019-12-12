using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    //Add this to an object to make it spin (in the z-axis)

    public float spinSpeedz;
    public float spinSpeedy;
    public float spinSpeedx;

    // Update is called once per frame
    void Update()
    {
            this.transform.Rotate(new Vector3(spinSpeedx, spinSpeedy * Time.deltaTime, spinSpeedz));
    }
}
