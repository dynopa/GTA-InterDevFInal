using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCarMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;

    public Vector3 velocity;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CourseChange"){
            Debug.Log("collided");
            rb.AddTorque(this.transform.up * turnSpeed, ForceMode.Force);
        }
        if(other.gameObject.tag == "StayOnCourse"){
            rb.AddTorque(-this.transform.up * turnSpeed, ForceMode.Force);
            this.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));

        }
    }
}
