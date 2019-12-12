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

    [Header("Footsteps")]
    public AudioSource footsteps;
    [Range(0,1f)]
    public float maxVolume;
    [Range(0,1f)]
    public float minVolume;
    [Space]
    public float maxSpeedForVolume;
    [Space]
    [Range(0,1f)]
    public float footstepRaiseVolSpeed;

    float currentFootstepVolume;

    void FixedUpdate()
    {
        Vector3 pos = PlayerManager.Instance.rb.transform.position;
        Vector3 input = new Vector3((Input.GetAxis("Horizontal")), 0, (Input.GetAxis("Vertical")));
        if (Mathf.Abs(input.magnitude) < .01f)
        {
            input = Vector3.zero;
        }
        Vector3 newVel;
        if (input.magnitude > 1)
        {
            newVel = ((input.normalized * moveSpeed));
        }
        else
        {
        newVel = (input * moveSpeed);
        }
        PlayerManager.Instance.rb.velocity = newVel;

        if (!PlayerManager.Instance.inCar)
        {
            //Audio
            float expectedFootstepVolume = PlayerManager.Instance.rb.velocity.magnitude.Remap(0, maxSpeedForVolume, minVolume, maxVolume);
            currentFootstepVolume = currentFootstepVolume + (footstepRaiseVolSpeed * (expectedFootstepVolume - currentFootstepVolume));
            footsteps.volume = currentFootstepVolume;
        }
        else footsteps.volume = 0;

        //Rotation Adjust
        if (input.magnitude > .15f)
        {
            Quaternion suggestedRotation = Quaternion.LookRotation((newVel).normalized);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, suggestedRotation, rotationSnapSpeed);
            PlayerManager.Instance.rb.MoveRotation(newRotation);
        }


    }
}
