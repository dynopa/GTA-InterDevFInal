using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    public int health;

    float healthTimer = 0;

    int oldHealthTemp = 0;

    bool recoveringHealth = false;


    // Start is called before the first frame update
    void Start()
    {
        //set up the health of this NPC
        //health = this.GetComponent<NpcCivPersonalityManager>().personality.health;

    }

    private void FixedUpdate()
    {

        healthTimer += Time.deltaTime;

        if (healthTimer >= 5f)
        {
            if ( oldHealthTemp <= health) {
                recoveringHealth = true;
            }
            else
            {
                recoveringHealth = false;
            }
            oldHealthTemp = health;
            healthTimer = 0;
        }



        if (recoveringHealth)
        {
            if (health < 50)
            {
                if (Time.frameCount % 3 == 0)
                {
                    health++;
                }
            }
            else
            {
                health = 50;
                recoveringHealth = false;
            }

        }


    }


    /// <summary>
    /// Reduces the health of the NPC for when they get shot. Takes in the damage dealt. After damage is dealt, checks if the NPC dies. If they are still alive, change their emotion to scared.
    /// </summary>
    /// <param name="damage">Damage.</param>
    public void ReduceHealth(int damage)
    {
        health -= damage;
        recoveringHealth = false;
        CheckForDeath();

    }

    /// <summary>
    /// Checks for the death of the NPC.
    /// </summary>
    private void CheckForDeath()
    {
        if (health <= 0)
        {
            //NpcCivManager.Instance.RemoveNpc(this.gameObject);
            this.gameObject.GetComponent<PlayerWalkMove>().enabled = false;
            this.gameObject.GetComponent<PlayerCarMove>().enabled = false;
            this.gameObject.transform.Translate(new Vector3(0, -1, 0));
            this.gameObject.transform.Rotate(new Vector3(70, 20, 0));
            //ScoreManager.Instance.IncreaseScore(10);
            //NpcCopManager.Instance.RemoveNpc(this.gameObject);
            //NpcCopManager.Instance.CopDeath();
            Invoke("StopForces", .5f);
            SceneManager.LoadScene("EndScene");
        }
        
    }

    /// <summary>
    /// Stops the forces acting on the NPC.
    /// </summary>
    private void StopForces()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
