using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static PlayerManager main;
    public static Vector3 position; //Just used to make players position an easier, concise reference (PlayerManager.position)

    //Component References
    [HideInInspector] public PlayerWalkMove playerWalkMove;
    [HideInInspector] public Rigidbody rb;





    private void Awake()
    {
        main = this;

        #region Add Player Script Components
        playerWalkMove = GetComponent<PlayerWalkMove>();
        rb = GetComponent<Rigidbody>();
        #endregion
    }

    void FixedUpdate()
    {
        position = rb.transform.position; //Just used to make players position an easier, concise reference (PlayerManager.position)
    }
}
