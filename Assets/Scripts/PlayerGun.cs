using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{

    public bool pulledOut;   
    public Gun currentGun;
    [Space]
    public bool autoReload;


    [HideInInspector]public int currentAmmo;
    

    MeshRenderer gunMR;
    PlayerInventory inventory;

    float timeUntilNextShot; //When less than 0, the player can fire;

    [HideInInspector]public float timeLeftOfReload;
    bool inReload;

    void Start()
    {
        gunMR = GetComponent<MeshRenderer>();
        inventory = GetComponentInParent<PlayerInventory>();
    }


    void Update()
    {
        currentAmmo = inventory.availableGuns[inventory.currentGunSelection].currentAmmo;

        Delays(); //Counts down the reload and fire rate timers



        //Complete Reload
        if (inReload && timeLeftOfReload < 0)
        {
            inReload = false;
            inventory.availableGuns[inventory.currentGunSelection].currentAmmo = currentGun.clipSize;
        }

    }



    void Delays() //Used to delay gun shots and reload speeds
    {
        timeUntilNextShot -= Time.deltaTime;
        timeLeftOfReload -= Time.deltaTime;
    }
    
    public void Reload()
    {
        if (!inReload && currentAmmo < currentGun.clipSize)
        {
            inReload = true;
            timeLeftOfReload = currentGun.reloadTime;
        }
    }

    public void GunFire(Gun usedGun, Vector3 dir) // Shoots a bullet of a given gun in a given direction
    {
        if (pulledOut && timeUntilNextShot < 0 && currentAmmo > 0 && !inReload)
        {

            GameObject bulletInstance = Instantiate(usedGun.bulletType, transform.position, Quaternion.LookRotation(dir));
            BulletMove bulletParameters = bulletInstance.GetComponent<BulletMove>();

            bulletParameters.speed = usedGun.bulletSpeed;
            bulletParameters.damage = usedGun.bulletDamage;
            bulletParameters.force = usedGun.bulletForce;


            bulletParameters.explosive = usedGun.explosive;
            bulletParameters.explosionRadius = usedGun.explosionRadius;
            bulletParameters.upForceMod = usedGun.upForce;

            timeUntilNextShot = currentGun.fireRate;
            inventory.availableGuns[inventory.currentGunSelection].currentAmmo--;

        }
    }    

    public void ToggleGunOut() //Toggles whether the gun is pulled out or not
    {
        pulledOut = !pulledOut;
        gunMR.enabled = !gunMR.enabled;
    }

}
