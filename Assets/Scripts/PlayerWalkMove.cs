using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    [Header("Player Rotation")]
    Quaternion suggestedRotation;
    [Range(0f, 1f)]
    public float rotationSnapSpeed;

    void FixedUpdate()
    {
        Vector3 pos = PlayerManager.Instance.rb.transform.position;
        Vector3 input = new Vector3((Input.GetAxis("Horizontal")), 0, (Input.GetAxis("Vertical")));
        if (Mathf.Abs(input.magnitude) < .01f)
        {
            input = Vector3.zero;
        }

        Vector3 newVel = (input * moveSpeed);
        PlayerManager.Instance.rb.velocity = newVel;

        //Rotation Adjust
        if (input.magnitude > .2f)
        {
            Quaternion suggestedRotation = Quaternion.LookRotation((newVel).normalized);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, suggestedRotation, rotationSnapSpeed);
            PlayerManager.Instance.rb.MoveRotation(newRotation);
        }


    }
}
