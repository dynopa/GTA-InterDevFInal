using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager Instance;
    public static Vector3 position; //Just used to make players position an easier, concise reference (PlayerManager.position)

    [Header("Gun Inputs")]
    public KeyCode gunToggleInput;
    [Space]
    public KeyCode gunSelectLeftInput;
    public KeyCode gunSelectRightInput;
    [Space]
    public KeyCode fireGunInput;
    [Space]
    public KeyCode reloadInput;


    //Component References
    [HideInInspector] public PlayerWalkMove playerWalkMove;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PlayerInventory inventory;
    [HideInInspector] public PlayerGun playerGun;




    private void Awake()
    {
        Instance = this;

        #region Add Player Script Components
        playerWalkMove = GetComponent<PlayerWalkMove>();
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
        playerGun = GetComponentInChildren<PlayerGun>();
        #endregion
    }


    private void Update()
    {

        #region Gun Inputs
        //Toggle weapon out or not  
        if (Input.GetKeyDown(gunToggleInput)) 
        {
            playerGun.ToggleGunOut();
        }

        //Switch gun
        if (Input.GetKeyDown(gunSelectLeftInput))
        {
            inventory.GunSelectLeft();
        }
        if (Input.GetKeyDown(gunSelectRightInput))
        {
            inventory.GunSelectRight();
        }
        
        //Fire weapon
        if (Input.GetKeyDown(fireGunInput))
        {
            playerGun.GunFire(playerGun.currentGun, transform.forward);
        }
        if (Input.GetKey(fireGunInput) & playerGun.currentGun.fullyAuto)
        {
            playerGun.GunFire(playerGun.currentGun, transform.forward);
        }

        if (Input.GetKeyDown(reloadInput))
        {
            playerGun.Reload();
        }
        #endregion

        playerGun.currentGun = inventory.availableGuns[inventory.currentGunSelection].gunType;

    }


    void FixedUpdate()
    {
        position = rb.transform.position; //Just used to make players position an easier, concise reference (PlayerManager.position)
        
    }
}
