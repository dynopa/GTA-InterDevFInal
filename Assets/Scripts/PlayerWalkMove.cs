using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("Player Rotation")]
    Quaternion suggestedRotation;
    [Range(0f,1f)]
    public float rotationSnapSpeed; 
 
    void FixedUpdate()
    {
        Vector3 pos = PlayerManager.position;
        Vector3 input = new Vector3((Input.GetAxis("Horizontal")), 0, (Input.GetAxis("Vertical")));
        if (input.magnitude < .02f)
        {
            input = Vector3.zero;
        }

        Vector3 newPos = pos + (input * moveSpeed * Time.fixedDeltaTime);
        PlayerManager.main.rb.MovePosition(newPos);

        //Rotation Adjust
        if (input.magnitude > .2f)
        {
            Quaternion suggestedRotation = Quaternion.LookRotation((newPos - pos).normalized);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, suggestedRotation, rotationSnapSpeed);
            PlayerManager.main.rb.MoveRotation(newRotation);
        }

        PlayerManager.main.rb.MovePosition(new Vector3(PlayerManager.main.rb.position.x, 0, PlayerManager.main.rb.position.z));



    }
}
