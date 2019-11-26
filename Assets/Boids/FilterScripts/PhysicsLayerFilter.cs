using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original)
        {
            //check to see if your object is on the same layer as your layer mask
            //if the thing in the mask and the one on the other side of the "ORed with" are on the same layers, then it will be true,
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
