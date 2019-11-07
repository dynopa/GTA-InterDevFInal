using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NpcCivSpawn.Instance.AddRandomPoints();
        NpcCivSpawn.Instance.SpawnAtAllCoords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
