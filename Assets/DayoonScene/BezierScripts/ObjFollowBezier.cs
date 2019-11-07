using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowBezier : MonoBehaviour {

    Transform currentRoute;
    float bezLength; //Length of the actual bezier line

    private Vector3 playerPos;  
    public bool enRoute; //Whether mid path or not

    [Range(0,15)]
    public float speed; //Speed at which the player is supposed to move along the bezier.
    [Range(1, 10)]
    public float changePathSpeed; //Keeps the player from jumping rapidly between beziers when one ends.




	void Update () {

        if (!enRoute) //If not in a bezier path, look for a new one and follow it
        {
            currentRoute = NearestBezier(transform.position);
            StartCoroutine(FollowRoute(currentRoute));
        }

	}



    public Vector3 PositionAhead(float t)
    {
     
        if (currentRoute.GetComponent<QuadraticBezier>() != null)
        {
                QuadraticBezier currentBezier = currentRoute.gameObject.GetComponent<QuadraticBezier>();
            
                if (t < 0)
                {
                    t = 0;
                }
                if (t > 1)
                {
                    t = 1;
                }
                Vector3 returnVector = currentBezier.GetPositionFromCompletionPercentage(t);
                return returnVector;
            
        }
        else
        {
            return playerPos;
        }
    }

    IEnumerator FollowRoute(Transform route)
    {
        //Starts Route, creates the points p0 (Start Pos) p1 (Bezier Pos) and p2 (End Pos)
        enRoute = true;
        QuadraticBezier currentBezier = currentRoute.gameObject.GetComponent<QuadraticBezier>();
        Vector3 initialPos = currentBezier.controlPoints[0].position;
        bezLength = currentBezier.bezierLength;

        if (Mathf.Abs((transform.position - initialPos).magnitude) > 2)
        {
            while (Mathf.Abs((transform.position - initialPos).magnitude) > .1f)
            {
                transform.position = Vector3.Lerp(transform.position, initialPos, changePathSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

        }

        float t = 0; 
        while (t < 1)
        {
            
            t += Time.deltaTime / bezLength * speed; //Move along any length of bezier at consistent speed
            playerPos = currentBezier.GetPositionFromCompletionPercentage(t);
            transform.position = playerPos;
            // //Absolute max speed 
            yield return new WaitForEndOfFrame();

        }
        enRoute = false;
        yield return null;
    }

    Transform NearestBezier(Vector3 position)
    {
        //finding the next or nearest beziers to follow

        GameObject[] beziers;
        beziers = GameObject.FindGameObjectsWithTag("Bezier");
        //marks the startpos
        GameObject closest = null;
        float distance = 100;
        if (beziers.Length == 0)
        {
            Debug.LogError("No beziers to track to!");
        }

        //Pass 1: Checks for as many bezier start points that are super close to it as it can find, and randomizes which one it decides to go on
        List<GameObject> bestPossibilities = new List<GameObject>(); //If two path starts are really close to each other it will add both to the list and randomize between them
        foreach (GameObject bezier in beziers)
        //overlapping bezier points

        {
            Vector3 bezStartPos = bezier.GetComponent<QuadraticBezier>().controlPoints[0].position;
            Vector3 diff = bezStartPos - position;
            float currDistance = diff.magnitude;
            if (currDistance < .25f) 
            {
                bestPossibilities.Add(bezier);
            }
            //if there are no other nearby or overlapping beziers, use this bezier
        }
        if (bestPossibilities.Count > 0)
        {
            int chooseNextPath = Random.Range(0, bestPossibilities.Count);
            return bestPossibilities[chooseNextPath].transform;          
        }
        //Randomly choosing between two different beziers
        
        //Pass 2: Checks for the nearest bezier script, regardless of how far it is
        else
        {
                foreach (GameObject bezier in beziers)
                {
                    Vector3 bezStartPos = bezier.GetComponent<QuadraticBezier>().controlPoints[0].position;
                    Vector3 diff = bezStartPos - position;
                    float currDistance = diff.magnitude;
                    if (currDistance < distance)
                    {
                        closest = bezier;
                        distance = currDistance;
                    }
                }

                if (closest == null)
            {
                return currentRoute;
            }
            else
            {
                return closest.transform;
            }
        }
        
    }
}
