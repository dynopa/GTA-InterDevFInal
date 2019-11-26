using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //"?" = check if true, ":" = otherwise
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        //averaging; calculates the middle point of all the neighbors
        cohesionMove /= context.Count;

        //currently a global position; must make it into the actual offset of the agent itself
        //by creating offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;

        //finding the middle point of all the neighbors and tries to move there
    }
}
