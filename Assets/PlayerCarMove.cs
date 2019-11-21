using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public float backupSpeed;
    [Space]
    public float accelerateRate; //Speed at which it accelerates
    public float deccelerateRate; //"" for decceleration
    float currentAcceleration;

    [Header("Car Rotation")]
    Quaternion suggestedRotation;
    [Range(0f, 1f)]
    public float rotationSnapSpeed;

    [Header("Car Interactions")]
    public float carDestroyThreshold; //The velocity magnitude at which the player needs to go when hitting another car to total it
    public float carLaunchForce;

    void FixedUpdate()
    {
        Vector3 pos = PlayerManager.Instance.rb.transform.position;
        





        if (Input.GetKey(KeyCode.W))
            {
            //Adds an accelerated movespeed in the direction the car is facing
            PlayerManager.Instance.rb.AddForce(transform.forward  * moveSpeed * currentAcceleration * 100);
            }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerManager.Instance.rb.AddForce(-transform.forward * backupSpeed * 100);
        }
        if (PlayerManager.Instance.rb.velocity.magnitude > .1f)//Prevent turning if the car is stopped
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                PlayerManager.Instance.rb.AddTorque(0, -turnSpeed *deccelerateRate * 100, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                PlayerManager.Instance.rb.AddTorque(0, turnSpeed * deccelerateRate * 100, 0);
            }
        }

        //Accelerator
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (currentAcceleration < 1)
            {
                currentAcceleration += accelerateRate * Time.fixedDeltaTime;
            }
            
        }
        else
        {
            if (currentAcceleration > 0)
            {
                currentAcceleration -= deccelerateRate * Time.fixedDeltaTime;
            }
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car") && PlayerManager.Instance.rb.velocity.magnitude >carDestroyThreshold)
        {
           Destroy( collision.gameObject.GetComponent<CarFollowBezier>() );
           Destroy( collision.gameObject.GetComponent<CarAlignToBezier>() );
            Rigidbody hitCarRB = collision.gameObject.GetComponent<Rigidbody>();
            hitCarRB.AddExplosionForce(carLaunchForce, PlayerManager.Instance.rb.transform.position, 4);
        }
    }
}
