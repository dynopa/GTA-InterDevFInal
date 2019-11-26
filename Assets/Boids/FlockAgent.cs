using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    Flock agentFlock;
    public Flock AgentFlock { get {return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get  {return agentCollider;}}

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        //turn the agent so it faces the direction it will move towards
        //also moves the agent in that direction

        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
        //constant movement regardless of framerate
    }
}
