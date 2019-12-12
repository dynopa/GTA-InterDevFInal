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
    public float carUpForce;

    [Header("Car Physics")]
    public float mass;
    public float drag;
    public float angularDrag;

    [Header("Car Audio")]
    public AudioClip[] sfx_hitCar;
    public AudioClip[] sfx_hitPerson;
    public AudioClip[] sfx_hitObstacle;

    [HideInInspector] public Rigidbody rb;

    [Header("Engine Sound Effects")]
    public AudioSource carRevving;
    [Space]
    public float maxCarSpeedToRev;
    [Space]
    [Range(0, 1f)]
    public float minVol;
    [Range(0, 1f)]
    public float maxVol;
    [Space]
    [Range(.8f, 1.4f)]
    public float minPitch;
    [Range(.8f, 1.4f)]
    public float maxPitch;
    [Space]
    [Range(0,1f)]
    public float volAccelerate;
    [Range(0, 1f)]
    public float volDecelerate;
    [Space]
    [Range(0, 1f)]
    public float pitchAccelerate;
    [Range(0, 1f)]
    public float pitchDecelerate;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = rb.transform.position;



        float expectedVolume = rb.velocity.magnitude.Remap(0, maxCarSpeedToRev, minVol, maxVol);
        float expectedPitch = rb.velocity.magnitude.Remap(0, maxCarSpeedToRev, minPitch, maxPitch);

        if (carRevving.volume < expectedVolume)
        {
            carRevving.volume = carRevving.volume + (volAccelerate * (expectedVolume - carRevving.volume));
            carRevving.pitch = carRevving.pitch + (pitchAccelerate * (expectedPitch - carRevving.pitch));
        }
        else
        {
            carRevving.volume = carRevving.volume + (volDecelerate * (expectedVolume - carRevving.volume));
            carRevving.pitch = carRevving.pitch + (pitchAccelerate * (expectedPitch - carRevving.pitch));
        }


        if (Input.GetKey(KeyCode.W))
            {
            //Adds an accelerated movespeed in the direction the car is facing
            rb.AddForce(transform.forward  * moveSpeed * currentAcceleration * 1000);
            }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * backupSpeed * 1000);
        }
        if (rb.velocity.magnitude > .1f)//Prevent turning if the car is stopped
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(0, -turnSpeed * Mathf.Pow(currentAcceleration,2) * 1000, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(0, turnSpeed * Mathf.Pow(currentAcceleration,2) * 1000, 0);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Car") && PlayerManager.Instance.rb.velocity.magnitude > carDestroyThreshold && PlayerManager.Instance.inCar)
        {

            collision.transform.gameObject.layer = LayerMask.NameToLayer("Obstacles");


            Destroy(collision.gameObject.GetComponent<CarFollowBezier>());
            Destroy(collision.gameObject.GetComponent<CarAlignToBezier>());
            Rigidbody hitCarRB = collision.gameObject.GetComponent<Rigidbody>();
            hitCarRB.constraints = RigidbodyConstraints.None;
            hitCarRB.useGravity = true;
            hitCarRB.isKinematic = false;
            float velocIncreaseForceMultiplier = Mathf.Sqrt(PlayerManager.Instance.rb.velocity.magnitude) * 10;


            NpcCopManager.Instance.IncreaseStarScore(15);

            ParticleManager.Instance.InstantiateExplosion(hitCarRB.transform.position, hitCarRB.transform);
            hitCarRB.AddExplosionForce(carLaunchForce * velocIncreaseForceMultiplier, PlayerManager.Instance.rb.transform.position, 15, carUpForce * velocIncreaseForceMultiplier, ForceMode.Impulse);


            //Audio
            SoundEffectManager.Instance.PlaySoundEffect(sfx_hitCar[Random.Range(0, sfx_hitCar.Length)], Random.Range(.55f, .7f), true);

        }



        if (collision.transform.gameObject.GetComponent<NpcCivDeath>() != null)
        {
            if (this.enabled)
            {
                collision.transform.gameObject.GetComponent<NpcCivDeath>().ReduceHealth(100);
                SoundEffectManager.Instance.PlaySoundEffect(sfx_hitPerson[Random.Range(0, sfx_hitPerson.Length)], Random.Range(.15f, .3f), true);
                ParticleManager.Instance.InstantiateBloodSquirt(collision.transform.position, collision.transform);

            }
        }
        else if (collision.transform.gameObject.GetComponent<NpcCopDeath>() != null)
            {
                if (this.enabled)
                {
                    collision.transform.gameObject.GetComponent<NpcCopDeath>().ReduceHealth(100);
                    SoundEffectManager.Instance.PlaySoundEffect(sfx_hitPerson[Random.Range(0, sfx_hitPerson.Length)], Random.Range(.15f, .3f), true);
                ParticleManager.Instance.InstantiateBloodSquirt(collision.transform.position, collision.transform);
            }
            }
        else if (collision.transform.gameObject.layer == 21 && Random.Range(0,1f) > .4f)
        {
            SoundEffectManager.Instance.PlaySoundEffect(sfx_hitPerson[Random.Range(0, sfx_hitPerson.Length)], Random.Range(.1f, .2f), true);
        }

    }
}
