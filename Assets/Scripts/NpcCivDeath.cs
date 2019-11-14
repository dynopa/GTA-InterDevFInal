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
    /// Reduces the health of the NPC for when they get shot. Takes in the damage dealt. After damage is dealt, checks if the NPC dies. If they are still alive, change their emotion to scared.
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void ReduceHealth (int damage)
    {
         health -= damage;
         if (!CheckForDeath())
        {
            this.GetComponent<NpcCivPersonalityManager>().currentEmotion = this.GetComponent<NpcCivPersonalityManager>().frightened;
        }
    }

    /// <summary>
    /// Checks for the death of the NPC.
    /// </summary>
    private bool CheckForDeath() {
        if (health <= 0)
        {
            NpcCivManager.Instance.RemoveNpc(this.gameObject);
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

}
