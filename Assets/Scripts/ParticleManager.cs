using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject explosionSmoke;
    public static ParticleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void InstantiateExplosion(Vector3 pos, Transform parent)
    {
        Instantiate(explosionSmoke, pos, Quaternion.identity, parent);
    }

}
