using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    
    //creates a slider
    [Range(1, 500)]
    //default count
    public int startingCount = 250;
    //populating it randomly within a circle
    const float AgentDensity = 0.08f;
    //how the flock behaves; how fast it moves; adding adjustability; "drive factor" = establishes a multiplication and moves it faster
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range (1f, 100f)]
    //making two radiuses; 1. directly raiduses for the neighbors, 2. a multiplier to say how much smaller is the radius for the avoidance
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range (0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    //we need access to avoidance radius

    public float SquareAvoidanceRadius {get {return squareAvoidanceRadius;}}


    // Start is called before the first frame update
    void Start()
    {
        //instead of finding the magnitude, we compare the squares of each other
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //initializing and instantiating the flock
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                //instantiating in a random point in a circle
                Random.insideUnitCircle * startingCount * AgentDensity,
                //the size of the circle is based on the number of agents we have in the flock; not so far apart or close so that they are on top of each other; checking for collision and distance
                //setting the rotation set in the random value between 0 to 60, rotated on its z-axis, so it will be spinning in some direction like a clock
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            //"I belong in a flock, and this is the particular flock I belong to"
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            // Debugging whether getting nearby object works:
            // agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            //behavior will take over, the scriptableobject will run whatever calculations it needs to, and return back a vector2 (this is the way that the agent should move)
            move *= driveFactor;
            //will make a speedier movement
            //check for whether we have exceeded our maximum speed, if so pull back and caps out at maxSpeed
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
                //reset it back to magnitude of 1 and multiply it to the maxSpeed
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        //with unity 3D, getting an array of colliders and Physics.OverlapSphereAll
        foreach(Collider2D c in contextColliders)
        {
            //AgentCollider saves us from using GetComponent every single time
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
