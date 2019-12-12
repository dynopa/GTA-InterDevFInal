using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject explosionSmoke;
    public GameObject bloodSquirt;
    public AudioClip[] bloodSplatters;

    public static ParticleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void InstantiateExplosion(Vector3 pos, Transform parent)
    {
        Instantiate(explosionSmoke, pos, Quaternion.identity, parent);
    }

    public void InstantiateBloodSquirt(Vector3 pos, Transform parent)
    {
        if (Random.Range(0f, 1f) > .4f)
        {
            pos.y = 1.01f;
            Instantiate(bloodSquirt, pos, Quaternion.identity, parent);
            SoundEffectManager.Instance.PlaySoundEffect(bloodSplatters[Random.Range(0, bloodSplatters.Length)], 1, true); 
        }
    }

}
