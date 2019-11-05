using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivMoveWalk : MonoBehaviour
{
    float maxDist = 7f;
    float walkSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward(walkSpeed);

        if (RayCastForward())
        {
            this.transform.LookAt(DecideNewDirection(RayCastDirections()));
        }
    }

    public void MoveForward (float speed) {
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }



    public bool RayCastForward()
    {
        Ray rayCheck = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (!Physics.Raycast(rayCheck, out hit, maxDist))
        {
            return true;
        }

        return false;
    }

    public Vector3 DecideNewDirection (List<Vector3> collisions)
    {
        List<Vector3> rayDirections = new List<Vector3>();
        rayDirections.Add(this.transform.right);
        rayDirections.Add( -this.transform.right);
        rayDirections.Add(-this.transform.forward);

        foreach (Vector3 col in collisions)
        {
            foreach (Vector3 dir in rayDirections)
            {
                if (col.Equals(dir))
                {
                    rayDirections.Remove(dir);
                }
            }
        }

        return rayDirections[0];
    }

    public List<Vector3> RayCastDirections() {

        List<Vector3> listOfCollisions = new List<Vector3>();

        Vector3[] rayDirections = new Vector3[3];
        rayDirections[0] = this.transform.right;
        rayDirections[1] = -this.transform.right;
        rayDirections[2] = -this.transform.forward;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Ray rayCheck = new Ray(this.transform.position, rayDirections[i]);
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(rayCheck, out hit, maxDist))
            {
                listOfCollisions.Add(rayDirections[i]);
            }
        }

        return listOfCollisions;
    }

}
