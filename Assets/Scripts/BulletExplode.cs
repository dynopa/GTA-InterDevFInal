using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplode : MonoBehaviour
{
    ParticleSystem pSystem;
    float timeLeft;

    private void Start()
    {

        pSystem = GetComponent<ParticleSystem>();
        
        timeLeft = 1;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(this.gameObject);
        }


    }
}
