using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    


    public bool pulledOut;
    MeshRenderer gunMR;
    public Gun currentGun;  

    float timeUntilNextShot;

    void Start()
    {
        gunMR = GetComponent<MeshRenderer>();
        pulledOut = false;
        gunMR.enabled = false;
    }


    void Update()
    {
        timeUntilNextShot -= Time.deltaTime;

        //Toggle WeaponOut

        if (Input.GetKeyDown(KeyCode.Z)) //Toggle weapon out or not
        {
            if (pulledOut)
            {
                pulledOut = false;
                gunMR.enabled = false;
            }
            else if (currentGun != null)
            {
                pulledOut = true;
                gunMR.enabled = true;
            }
        }

        if (pulledOut && Input.GetKeyDown(KeyCode.Space) && timeUntilNextShot < 0)
        {
            GunFire(currentGun, transform.forward);
        }
    }


    void GunFire(Gun usedGun, Vector3 dir)
    {
        GameObject bulletInstance = Instantiate(usedGun.bulletType, transform.position, Quaternion.LookRotation(dir));
        BulletMove bulletParameters = bulletInstance.GetComponent<BulletMove>();

        bulletParameters.speed = usedGun.bulletSpeed;
        bulletParameters.damage = usedGun.bulletDamage;
    }
}
