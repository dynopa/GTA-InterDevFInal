using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{

    //Quadratic beziers use a start point, an end point, and a single bezier point to draw the curve.


    //The formula for calculating a point on a quadratic bezier is (Vector3.Position = bezierPos + ( (1-t)^2 * (startPos - bezierPos) ) + ( t^2 * (endPos - bezierPos) )
    //
    //More info: https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B%C3%A9zier_curves
    public bool isStraightLine;
        [Space]
    [Header("Stoplight Management")]
    public bool isStoplightNode;
    public int stopLightOrder;

    float tStepBetweenMidPoints = .05f; //Distance (in percentage of total bezier) between each midpoint. (ONLY FOR DRAWING VISUALIZATION GIZMOS)   
    [Space]
    public Transform[] controlPoints;

    

    Vector3 gizmoPos;
    [HideInInspector] public float bezierLength;
    [HideInInspector] public Vector3[] points = new Vector3[3];

    private void Awake()
    {
        tStepBetweenMidPoints = .05f;
        points = new Vector3[3];
        points[0] = controlPoints[0].position;
        points[1] = controlPoints[1].position;
        points[2] = controlPoints[2].position;
        GetBezierLength();
        
    }

    private void Start()
    {
        
     tStepBetweenMidPoints = .15f;
       
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Draws bezier preview
    private void OnDrawGizmos()
    {
        //if (Application.isEditor)
        //{
        //points = new Vector3[3];
        //points[0] = controlPoints[0].position;
        //points[1] = controlPoints[1].position;
        //points[2] = controlPoints[2].position;

        //Vector3 secondToLastPos = Vector3.zero;
        //bezierLength = 0;
        //Gizmos.color = Color.grey;
        ////Draws the lines that display that show the scope of the bezier (start to bezier pos, and bezier to endPos lines)
        //Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        //Gizmos.DrawLine(controlPoints[1].position, controlPoints[2].position);


        ////Draws lines between spheres along the bezier with spacing between each sphere as the tStepBetweenMidPoints.
        //for (float t = 0; t <= 1; t += tStepBetweenMidPoints)
        //{
        //    Gizmos.color = Color.yellow;
        //    float nextT = t + tStepBetweenMidPoints;

        //    gizmoPos = this.GetPositionFromCompletionPercentage(t);

        //    if (nextT < 1 && nextT > 0)
        //    {

        //        Vector3 nextPos = this.GetPositionFromCompletionPercentage(nextT);
        //        Gizmos.DrawLine(gizmoPos, nextPos);
        //        bezierLength += Vector3.Distance(gizmoPos, nextPos);

        //    }

        //    secondToLastPos = gizmoPos;

        //}
        //Gizmos.DrawLine(secondToLastPos, points[2]);
        //bezierLength += Vector3.Distance(secondToLastPos, points[2]);

        //}

    }


    public void CenterBezierPoint() //Centers the bezier point between the start and end positions
    {
        controlPoints[1].position = (controlPoints[0].position + controlPoints[2].position) / 2;
    }


    public void RightAngleBezierPoint1() //Moves the bezier point to form a right angle between the start and end positions
    {
        controlPoints[1].position = new Vector3(controlPoints[0].position.x, 0, controlPoints[2].position.z);
    }

    public void RightAngleBezierPoint2()
    {
        controlPoints[1].position = new Vector3(controlPoints[2].position.x, 0, controlPoints[0].position.z);
    }

    public bool isTurn()
    {
        Vector3 movementVector = points[2] - points[0];
        if (Mathf.Abs(movementVector.x) > 1 && Mathf.Abs(movementVector.z) > 1)
        {
            return true;
        }
        else return false;
                
    }


    public void GetBezierLength()
    {
        bezierLength = 0;
       

        //Draws lines between spheres along the bezier with spacing between each sphere as the tStepBetweenMidPoints.
        
            
            Vector3 secondToLastPos = Vector3.zero;
            
            for (float pt = 0; pt <= 1; pt += tStepBetweenMidPoints)
            {
                
                float nextT = pt + tStepBetweenMidPoints;

                gizmoPos = this.GetPositionFromCompletionPercentage(pt);

                if (nextT < 1 && nextT > 0)
                {
                    Vector3 nextPos = this.GetPositionFromCompletionPercentage(nextT);
                    bezierLength += Vector3.Distance(gizmoPos, nextPos);

                }

                secondToLastPos = gizmoPos;
            }
            bezierLength += Vector3.Distance(secondToLastPos, points[2]);
        

        if (isStraightLine)
        {
            bezierLength = Vector3.Distance(points[0], points[2]);
        }
    }

}

