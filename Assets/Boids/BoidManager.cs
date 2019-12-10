using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject Steve;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(Steve, Random.insideUnitSphere * 10, Quaternion.identity);
        }
    }

}
