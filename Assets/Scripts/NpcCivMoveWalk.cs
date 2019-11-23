using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivMoveWalk : MonoBehaviour
{
    [Header("Ray Cast Distance")]
    [SerializeField]float maxDist = 30f;

    private int forwardAngleRandomizer;

    //private bool notOnSidewalk;
    //private Vector3 previousSidewalkPos = new Vector3(0, 0, 0);
    private void Start()
    {
        forwardAngleRandomizer = (int)Random.Range(2, 5);
    }


    // Update is called once per frame
    void Update()
    {

        if (this.GetComponent<NpcCivPersonalityManager>().currentEmotion == this.GetComponent<NpcCivPersonalityManager>().frightened)
        {
            LookAwayFromPlayer();
        }
        else
        {
            GameObject inFront = RayCastDown();
            if (inFront != null && inFront.tag != null)
            {
                if (inFront.tag != "Concrete")
                {
                    List<Vector3> angledDirectionPossibilities = RayCastDirectionsAngled();
                    Debug.Log(angledDirectionPossibilities.Count);
                    if (angledDirectionPossibilities.Count != 3)
                    {
                        this.transform.LookAt(this.transform.position + DecideNewDirection(angledDirectionPossibilities));
                    }


                }
            }
        }

        //NPC constantly moves forward
        //When they detect a collider ahead of them, they choose a new valid directions and turn that way
        //Turns right, left, or back
        MoveForward(this.GetComponent<NpcCivPersonalityManager>().currentEmotion.speed);

        if (RayCastForward())
        {
            this.transform.LookAt(this.transform.position + DecideNewDirection(RayCastDirections()));
        }
    }

    /// <summary>
    /// Looks the away from player. For use when frightened to constantly move away from the player.
    /// </summary>
    private void LookAwayFromPlayer()
    {
        //Debug.Log(PlayerManager.Instance.gameObject.transform.position);
        this.transform.LookAt((this.transform.position - PlayerManager.Instance.gameObject.transform.position) * 1000);
    }


    /// <summary>
    /// Moves the NPC forward at a rate of speed.
    /// </summary>
    /// <param name="speed">Speed.</param>
    private void MoveForward (float speed) {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }



    /// <summary>
    /// Detects for colliders in the forward direction using a raycast.
    /// </summary>
    /// <returns><c>true</c>, if cast forward , <c>false</c> otherwise.</returns>
    private bool RayCastForward()
    {
        Ray rayCheck = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(rayCheck, out hit, maxDist))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Raycasts in front of this NPC at an angle and returns the object that connects with the raycast. Used to check what the NPC will walk on.
    /// </summary>
    /// <returns>The object in front of this NPC.</returns>
    public GameObject RayCastDown()
    {
        Ray rayCheck = new Ray(this.transform.position, -this.transform.up + (this.transform.forward * forwardAngleRandomizer));
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(this.transform.position, (-this.transform.up + (this.transform.forward * forwardAngleRandomizer)), Color.red);

        if (Physics.Raycast(rayCheck, out hit, maxDist))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Decides the new direction based on a given list of invalid collision directions.
    /// </summary>
    /// <returns>The new direction Vector3.</returns>
    /// <param name="collisions">Collisions.</param>
    private Vector3 DecideNewDirection (List<Vector3> collisions)
    {
        List<Vector3> rayDirections = new List<Vector3>();
        rayDirections.Add(this.transform.right);
        rayDirections.Add(-this.transform.right);
        rayDirections.Add(-this.transform.forward);

        List<Vector3> validDirections = new List<Vector3>();

        foreach (Vector3 dir in rayDirections)
        {
            bool isValid = true;
            foreach (Vector3 col in collisions)
            {
                if (col.Equals(dir))
                {
                    isValid = false;
                }
            }
            if(isValid)
                validDirections.Add(dir);
        }

        if (validDirections.Count > 0)
        {
            int r = Random.Range(0, validDirections.Count);
            return validDirections[r];
        }

        else
            return this.transform.forward;
       
    }


    /// <summary>
    /// Raycasts in the right, left, and back directions to detect for collisions.
    /// </summary>
    /// <returns>The valid directions that do not have collisions.</returns>
    private List<Vector3> RayCastDirections() {

        List<Vector3> listOfCollisions = new List<Vector3>();

        Vector3[] rayDirections = new Vector3[3];
        rayDirections[0] = this.transform.right;
        rayDirections[1] = -this.transform.right;
        rayDirections[2] = -this.transform.forward;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Ray rayCheck = new Ray(this.transform.position, rayDirections[i]);
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(this.transform.position, rayDirections[i] * maxDist, Color.cyan);
            if (Physics.Raycast(rayCheck, out hit, maxDist))
            {
                listOfCollisions.Add(rayDirections[i]);
            }
        }

        return listOfCollisions;
    }

    /// <summary>
    /// Raycasts in the right, left, and back directions at a downwards angle to detect for what surfaces are below this NPC in all directions.
    /// </summary>
    /// <returns>The valid directions that are tagged as walkable.</returns>
    private List<Vector3> RayCastDirectionsAngled()
    {

        List<Vector3> listOfCollisions = new List<Vector3>();

        Vector3[] rayDirections = new Vector3[3];
        rayDirections[0] = -this.transform.up + (this.transform.forward * forwardAngleRandomizer);
        rayDirections[1] = -this.transform.up + (this.transform.forward * forwardAngleRandomizer);
        rayDirections[2] = -this.transform.up + (this.transform.forward * forwardAngleRandomizer);

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Ray rayCheck = new Ray(this.transform.position, rayDirections[i]);
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(this.transform.position, rayDirections[i] * maxDist, Color.cyan);
            if (Physics.Raycast(rayCheck, out hit, maxDist))
            {
                if (hit.collider.gameObject.tag != "Concrete")
                {
                    listOfCollisions.Add(rayDirections[i]);
                }
            }
        }

        return listOfCollisions;
    }

}
