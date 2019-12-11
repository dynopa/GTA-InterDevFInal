using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.tag == "Train")
       {
        Debug.Log("Hit");
        Destroy(other.gameObject);
       }
     }
}
