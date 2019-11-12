﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float force;
    [HideInInspector] public float damage;

    [HideInInspector] public bool explosive;
    [HideInInspector] public float explosionRadius;
    [HideInInspector]public float upForceMod;

    public GameObject explosiveParticles;


    float distanceTraveled;
    float timeAlive;
    Vector3 forwardDir;

    Rigidbody rb;
   

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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            if (explosive)
            {
                Explosion(rb.transform.position, explosionRadius);
            }
            else
            {
                ShootBullet(collision, forwardDir.normalized * force, rb.transform.position);
            }

        }

        Destroy(this.gameObject);
    }

    void ShootBullet(Collision collision, Vector3 force, Vector3 position)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, position);
    }


    void Explosion(Vector3 pos, float radius)
    {
        Vector3 explosionPos = rb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        Instantiate(explosiveParticles, rb.transform.position, Quaternion.identity);


        foreach (Collider hitCollider in colliders)
        {
           
            Rigidbody hitRB = null;
            if (hitCollider.gameObject.GetComponent<Rigidbody>() != null)
            {
                hitRB = hitCollider.gameObject.GetComponent<Rigidbody>();
            }
            if (hitRB != null)
            {
                hitRB.AddExplosionForce(force, pos, radius, upForceMod, ForceMode.Impulse);
            }


        }
    }
}