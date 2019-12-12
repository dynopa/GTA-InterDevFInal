using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivDeath : MonoBehaviour
{

    [SerializeField] int health;


    // Start is called before the first frame update
    void Start()
    {
        //set up the health of this NPC

            health = 50;

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
            this.gameObject.GetComponent<NpcCivMoveWalk>().enabled = false;
            this.gameObject.transform.Translate(new Vector3(0, -1, 0));
            this.gameObject.transform.Rotate(new Vector3(70, 20, 0));
            //ScoreManager.Instance.IncreaseScore(10);
            NpcCopManager.Instance.CivDeath();
            Invoke("StopForces", .5f);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Stops the forces acting on the NPC.
    /// </summary>
    private void StopForces ()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
