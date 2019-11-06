using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivMoveWalk : MonoBehaviour
{
    [Header("Ray Cast Distance")]
    [SerializeField]float maxDist = 30f;

    [Header("Tuning")]
    [SerializeField] float walkSpeed = 10f;

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
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }



    public bool RayCastForward()
    {
        Ray rayCheck = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(rayCheck, out hit, maxDist))
        {
            return true;
        }

        return false;
    }

    public Vector3 DecideNewDirection (List<Vector3> collisions)
    {
        List<Vector3> rayDirections = new List<Vector3>();
        rayDirections.Add(this.transform.right);
        rayDirections.Add(-this.transform.right);

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
            return validDirections[0];

        else
            return this.transform.forward;
       
    }

    public List<Vector3> RayCastDirections() {

        List<Vector3> listOfCollisions = new List<Vector3>();

        Vector3[] rayDirections = new Vector3[2];
        rayDirections[0] = this.transform.right;
        rayDirections[1] = -this.transform.right;

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

}
