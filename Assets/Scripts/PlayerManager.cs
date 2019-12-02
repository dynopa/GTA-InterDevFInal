using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager Instance;
    public static Vector3 position; //Just used to make players position an easier, concise reference (PlayerManager.position)


    public bool inCar;
    public GameObject currentCar;
    public float carCheckRadius;

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
    [HideInInspector] public PlayerCarMove playerCarMove;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PlayerInventory inventory;
    [HideInInspector] public PlayerGun playerGun;
    [HideInInspector] public CapsuleCollider playerCollider;



    private void Awake()
    {
        Instance = this;
        
        #region Add Player Script Components
        playerWalkMove = GetComponent<PlayerWalkMove>();
        playerCarMove = GetComponent<PlayerCarMove>();
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
        playerGun = GetComponentInChildren<PlayerGun>();
        playerCollider = GetComponent<CapsuleCollider>();
        #endregion

        playerWalkMove.enabled = true;
        playerCarMove.enabled = false;
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


        if (Input.GetKeyDown(KeyCode.V) && !inCar)
        {
            FindCar();
        }
        else if (Input.GetKeyDown(KeyCode.V) && inCar)
        {
            ExitCar();
        }


        if (inCar)
        {
            playerWalkMove.enabled = false;
            playerCarMove.enabled = true;
            playerCollider.enabled = false;

            rb.mass = playerCarMove.mass;
            rb.drag = playerCarMove.drag;
            rb.angularDrag = playerCarMove.angularDrag;
        }
        else
        {

            playerWalkMove.enabled = true;
            playerCarMove.enabled = false;
            playerCollider.enabled = true;

            rb.mass = 1.8f;
            rb.drag = 1;
            rb.angularDrag = 2;
        }

    }


    void FixedUpdate()
    {
        position = rb.transform.position; //Just used to make players position an easier, concise reference (PlayerManager.position)
        
    }


    void FindCar()
    {
        Collider[] carChecks = Physics.OverlapSphere(transform.position, carCheckRadius);
        foreach (Collider carCheck in carChecks)
        {
            if (carCheck.gameObject.layer == 15)
            {
                EnterCar(carCheck);
                break;
            }
        }
    }

    public void EnterCar(Collider car)
    {
        inCar = true;
        currentCar = car.gameObject;
        Destroy(currentCar.GetComponent<CarFollowBezier>());
        Destroy(currentCar.GetComponent<CarAlignToBezier>());
        
        Destroy(currentCar.GetComponent<Rigidbody>());


        playerWalkMove.enabled = false;
        playerCarMove.enabled = true;

        playerCollider.enabled = false;

        transform.rotation = currentCar.transform.rotation;
        transform.position = new Vector3(currentCar.transform.position.x, transform.position.y, currentCar.transform.position.z);
        currentCar.transform.parent = this.transform;
    }

    void ExitCar()
    {
        inCar = false;
        currentCar.transform.parent = null;
        currentCar = null;
        transform.position += transform.right * 2;
    }
}
