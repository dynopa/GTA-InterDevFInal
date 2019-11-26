using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        //how many are in the avoidance radius
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //"?" = check if true, ":" = otherwise
        foreach (Transform item in filteredContext)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
                //automatically gives us the offset as well
            }
        }
        if (nAvoid > 0)
        
            avoidanceMove /= nAvoid;
    
        return avoidanceMove;

    }
}
