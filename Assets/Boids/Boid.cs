using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed;
    public Vector3 midpointDir;
    public Vector3 alignDir;
    public Vector3 randomDir;
    public LayerMask LM;
    public List<Boid> neighbors;

    [Range(0,1)] public float randomScalar;
    [Range(0,1)] public float cohesionScalar;
    [Range(0,1)] public float alignmentScalar;
    public float lookRange;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<Boid>();
        //Clear "if there's anything in my list, clear it out"
    }

    // Update is called once per frame
    void Update()
    {
        GetMyNeighbors();
        ComputeDirection();
        MoveBoid();
    }

    void GetMyNeighbors()
    {
        Collider[] n = Physics.OverlapSphere(transform.position, lookRange, LM, QueryTriggerInteraction.Collide);
        neighbors.Clear();
        foreach(Collider c in n)
        {
            neighbors.Add(c.GetComponent<Boid>());
        }
    }

    void ComputeDirection()
    {
        _Midpoint();
        _Align();
        _Random();
        direction = Vector3.Normalize(midpointDir * (cohesionScalar) + alignDir * (alignmentScalar) + randomDir * (randomScalar));
    }

    void MoveBoid()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 0.2f);
        transform.position += transform.forward * Time.deltaTime * speed;

        if (transform.position.y <= 10f)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        if (transform.position.y >= 50f)
        {
            transform.position -= Vector3.up * Time.deltaTime * speed;
        }
        if (transform.position.z >= 430f)
        {
            transform.position -= Vector3.forward * Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), Time.deltaTime * 15f);
        }
        if (transform.position.z <= -68f)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 15f);

        }
        if (transform.position.x >= 90f)
        {
            transform.position -= Vector3.right * Time.deltaTime * speed;

        }
        if (transform.position.x <= -88f)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;

        }
    }

    #region internalfunctions
    void _Midpoint()
    {
        if (neighbors.Count == 0)
        {
            midpointDir = transform.forward;
        } else {
            midpointDir = Vector3.zero;
            foreach (Boid b in neighbors)
            {
                midpointDir += b.transform.position;
                //sum all points
            }
            midpointDir *= (1/neighbors.Count);
            //avg the points by total
            midpointDir = midpointDir - transform.position;
            midpointDir = Vector3.Normalize(midpointDir);
        }
    }

    void _Align()
    {
       if (neighbors.Count == 0)
        {
            alignDir = transform.forward;
        } else {
            alignDir = Vector3.zero;
            foreach (Boid b in neighbors)
            {
                alignDir += b.transform.forward;
                //sum all points
            }
            alignDir *= (1/neighbors.Count);
            //avg the points by total
            alignDir = Vector3.Normalize(alignDir); 
        }
    }
    void _Random()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer -= 1;
            randomDir = Random.insideUnitSphere;
        }
    }

    #endregion
}
