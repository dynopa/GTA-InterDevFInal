using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCopShoot : MonoBehaviour
{




    public AudioSource[] audioSrc;
    int currentAudioIndex = 0;

    public GameObject bulletPrefab;
    public Gun currentGun;
    public bool isShooting = false;
    public float shootTimer = 0;
    public float shootThreshold = 1f;



    void Start()
    {

    }


    void FixedUpdate()
    {
        if (NpcCopManager.starLevel > 0)
        {
            if (Vector3.Distance(this.transform.position, PlayerManager.Instance.gameObject.transform.position) < 25f)
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;

            }
        }
        else
        {
            isShooting = false;

        }


        if (isShooting)
        {

            if (Time.frameCount % 180 <= 2)
            {
                GunFire(this.transform.forward);
                
            }
        }
    }



    public void GunFire(Vector3 dir) // Shoots a bullet of a given gun in a given direction
    {


            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(dir));
 


        
    }
    }

