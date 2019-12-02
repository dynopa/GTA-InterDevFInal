using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{

    //Quadratic beziers use a start point, an end point, and a single bezier point to draw the curve.


    //The formula for calculating a point on a quadratic bezier is (Vector3.Position = bezierPos + ( (1-t)^2 * (startPos - bezierPos) ) + ( t^2 * (endPos - bezierPos) )
    //
    //More info: https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B%C3%A9zier_curves
    [Header("Gizmos")]
    public bool enableGizmos;
    float tStepBetweenMidPoints = .15f; //Distance (in percentage of total bezier) between each midpoint. (ONLY FOR DRAWING VISUALIZATION GIZMOS)   
    [Space]
    public Transform[] controlPoints;
    
    Vector3 gizmoPos;
    [HideInInspector] public float bezierLength;

    private void Start()
    {
        tStepBetweenMidPoints = .15f;
    }

    //Draws bezier preview
    private void OnDrawGizmos()
    {
        if (enableGizmos)
        {
            Vector3 secondToLastPos = Vector3.zero;
            bezierLength = 0;
            Gizmos.color = Color.grey;
            //Draws the lines that display that show the scope of the bezier (start to bezier pos, and bezier to endPos lines)
            Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
            Gizmos.DrawLine(controlPoints[1].position, controlPoints[2].position);


            //Draws lines between spheres along the bezier with spacing between each sphere as the tStepBetweenMidPoints.
            for (float t = 0; t <= 1; t += tStepBetweenMidPoints)
            {
                Gizmos.color = Color.yellow;
                float nextT = t + tStepBetweenMidPoints;

                gizmoPos = this.GetPositionFromCompletionPercentage(t);

                if (nextT < 1 && nextT > 0)
                {
                    Vector3 nextPos = this.GetPositionFromCompletionPercentage(nextT);
                    bezierLength += Vector3.Distance(gizmoPos, nextPos);
                    Gizmos.DrawLine(gizmoPos, nextPos);
                }
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(gizmoPos, .1f);
                secondToLastPos = gizmoPos;
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(secondToLastPos, controlPoints[2].position); //Adds the final line from the last tStep sphere to the endPosition

            
        }
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


}

