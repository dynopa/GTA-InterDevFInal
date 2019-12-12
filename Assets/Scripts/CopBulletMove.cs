using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopBulletMove : MonoBehaviour
{

    public float speed;
    public float force;
    public int damage;

    public bool explosive;
    public float explosionRadius;
    public float upForceMod;

    public GameObject explosiveParticles;


    float distanceTraveled;
    float timeAlive;
    Vector3 forwardDir;

    Rigidbody rb;
    public AudioClip hitSound;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        forwardDir = transform.forward + new Vector3(Random.Range(-.1f, .1f), 0, Random.Range(-.1f, .1f));
        forwardDir.Normalize();
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

            ShootBullet(collision, forwardDir.normalized * force, rb.transform.position);

            SoundEffectManager.Instance.PlaySoundEffect(hitSound, Random.Range(.5f, .8f), true);
        }




        Destroy(this.gameObject);
    }

    void ShootBullet(Collision collision, Vector3 force, Vector3 position)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, position);

        if (collision.gameObject.GetComponent<NpcCivDeath>() != null)
        {
            collision.gameObject.GetComponent<NpcCivDeath>().ReduceHealth(damage);
        }
         else if (collision.gameObject.GetComponent<PlayerDeath>() != null)
        {

            if (PlayerManager.Instance.inCar)
            {
                collision.gameObject.GetComponent<PlayerDeath>().ReduceHealth(5);
            }
            else { collision.gameObject.GetComponent<PlayerDeath>().ReduceHealth(8); }
            
        }

    }


    void Explosion(Vector3 pos, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, radius);

        Instantiate(explosiveParticles, rb.transform.position, Quaternion.identity);

        print("explosion");

        foreach (Collider hitCollider in colliders)
        {

            Rigidbody hitRB = null;
            if (hitCollider.gameObject.GetComponent<Rigidbody>() != null)
            {
                hitRB = hitCollider.gameObject.GetComponent<Rigidbody>();
            }
            else if ((hitCollider.gameObject.GetComponentInParent<Rigidbody>() != null))
            {
                hitRB = hitCollider.gameObject.GetComponentInParent<Rigidbody>();
            }
            else if ((hitCollider.gameObject.GetComponentInChildren<Rigidbody>() != null))
            {
                hitRB = hitCollider.gameObject.GetComponentInChildren<Rigidbody>();
            }
            print(hitCollider.gameObject.name);

            if (hitRB != null)
            {
                float forceTmp = force;
                float upForceTmp = upForceMod;

                if (hitCollider.gameObject.GetComponent<NpcCivDeath>() != null)
                {

                    //Debug.Log("HitNPC");
                    hitCollider.gameObject.GetComponent<NpcCivDeath>().ReduceHealth(damage);
                    forceTmp *= 2;
                    upForceTmp *= 2;
                }
                else if (hitCollider.gameObject.GetComponent<NpcCopDeath>() != null)
                {

                    //Debug.Log("HitNPC");
                    hitCollider.gameObject.GetComponent<NpcCopDeath>().ReduceHealth(damage);
                    forceTmp *= 2;
                    upForceTmp *= 2;
                }

                if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Car"))
                {
                    DestroyCar(hitCollider);
                }


                hitRB.AddExplosionForce(forceTmp, pos, radius, upForceMod, ForceMode.Impulse);
            }


        }
    }

    void DestroyCar(Collider collision)
    {

        collision.transform.gameObject.layer = LayerMask.NameToLayer("Obstacles");


        Destroy(collision.gameObject.GetComponent<CarFollowBezier>());
        Destroy(collision.gameObject.GetComponent<CarAlignToBezier>());
        Rigidbody hitCarRB = collision.gameObject.GetComponent<Rigidbody>();
        hitCarRB.constraints = RigidbodyConstraints.None;
        hitCarRB.useGravity = true;
        hitCarRB.isKinematic = false;


        NpcCopManager.Instance.IncreaseStarScore(25);

        ParticleManager.Instance.InstantiateExplosion(hitCarRB.transform.position, hitCarRB.transform);
        hitCarRB.AddExplosionForce(force * 60, PlayerManager.Instance.rb.transform.position, 15, upForceMod * 10, ForceMode.Impulse);

    }
}
