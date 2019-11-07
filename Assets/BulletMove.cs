using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public float damage;
    float distanceTraveled;
    float timeAlive;
    Vector3 forwardDir;

    Rigidbody rb;


    bool shouldDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        forwardDir = transform.forward;
    }

    void FixedUpdate()      
    {
        Vector3 moveDistance = forwardDir * speed * Time.fixedDeltaTime;

        timeAlive += Time.fixedDeltaTime;
        distanceTraveled += moveDistance.magnitude;

        if (timeAlive > 1 || distanceTraveled > 40)
        {
            Destroy(this.gameObject);
        }

        rb.MovePosition(rb.transform.position + moveDistance);


        if (shouldDestroy)
        {

                Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        shouldDestroy = true;
    }
}
