using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector2 center;
    //defults to 0,0
    public float radius = 1f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //how far away is the agent from the center, and try to get back to the center
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        //if t = 0 at the center; if t = 1 at the radius; t > 1 beyond the radius
        if (t < 0.9f)
        {
            //as long as we are in the 90% of the radius, don't change
            //however if we are within the 10% or beyond, then pull us back
            return Vector2.zero;
        }
        return centerOffset * t * t;
        //the squaring gives a quadratic effect
    }
}
