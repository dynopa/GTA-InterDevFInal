using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    //using smooth damp that unity provides
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    //how long should the agent take to get from its current state to its calculated state


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        //conditioner operator to check; id we don't have a filter, use context as is, if we do have a filter, then apply that
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        //"?" = check if true, ":" = otherwise

        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        //averaging; calculates the middle point of all the neighbors
        cohesionMove /= filteredContext.Count;

        //currently a global position; must make it into the actual offset of the agent itself
        //by creating offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;

        //finding the middle point of all the neighbors and tries to move there
    }
}
