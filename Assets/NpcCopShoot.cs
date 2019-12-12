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

    bool inGunShootMode;

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
            if (!inGunShootMode)
            {

                inGunShootMode = true;
                StartCoroutine(ShootGun());
            }
        }
        else
        {
            inGunShootMode = false;
        }
    }


    public IEnumerator ShootGun()
    {
        yield return new WaitForSeconds(1 / Mathf.Sqrt((float)NpcCopManager.starLevel));
        float t = 0;
        while (t < .4f)
        {
            t += Time.deltaTime;
            this.gameObject.GetComponent<NpcCopMoveWalk>().LookAtPlayer();
            yield return new WaitForEndOfFrame();
        }

        GunFire(this.transform.forward);
        inGunShootMode = false;
        isShooting = false;
    }

    public void GunFire(Vector3 dir) // Shoots a bullet of a given gun in a given direction
    {


            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(dir));

    }
    }

