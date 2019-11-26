using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    //Combines all the cohesion, alignment, and avoidance behavior scripts

    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //handle data mismatch;checking for the two arrays to have the same information
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        //set up move
        Vector2 move = Vector2.zero;

        //iterate through behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
            //using for loop not foreach to make sure we are using the same index on the behavior and the weights

            //confirming that the partial move is being limited to the extent of the weight
            if(partialMove != Vector2.zero)
            {
                //if there is some movement being returned, does this overrall movement exceed the weight
                if(partialMove.sqrMagnitude > weights[i]* weights[i])
                {
                    //normalize it back to the magnitude of 1, and multiply it to the weight to set it to precisely the max of the weight
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                //otherwise, if the mag is less than the weight, then pass it in as normal
                move += partialMove;
            }
        }
        return move;
    }
}
