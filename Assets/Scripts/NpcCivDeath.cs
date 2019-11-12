using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivDeath : MonoBehaviour
{

    [SerializeField] int health;


    // Start is called before the first frame update
    void Start()
    {
        health = this.GetComponent<NpcCivPersonalityManager>().personality.health;
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Reduces the health of the NPC for when they get shot. Takes in the damage dealt. After damage is dealt, checks if the NPC dies.
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void ReduceHealth (int damage)
    {
         health -= damage;
         CheckForDeath();
    }

    /// <summary>
    /// Checks for the death of the NPC.
    /// </summary>
    private void CheckForDeath() {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
