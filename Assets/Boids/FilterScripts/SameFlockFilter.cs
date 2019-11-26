using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]

public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        //is it a flock agent, is it a member of the same flock; then add to the filter list
        foreach(Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            //if this item isn't flock agent, then null
            if(itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
                //taking in the original transform we are working in, checks that we have the right item to add
            }
        }
        return filtered;
    }
}
