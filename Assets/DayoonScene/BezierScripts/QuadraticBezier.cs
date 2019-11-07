using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{
    //https://en.wikipedia.org/wiki/B%C3%A9zier_curve#Quadratic_B%C3%A9zier_curves

    public bool enableGizmos;
    [Range(.05f, .499f)]
    float tStepBetweenMidPoints = .15f; //Distance (in percentage of total bezier) between each midpoi
    public Transform[] controlPoints;
    public float bezierLength;
    Vector3 gizmoPos;


    private void Start()
    {
        tStepBetweenMidPoints = .15f;
    }

    private void OnDrawGizmos()
    {
        if (enableGizmos)
        {
            Vector3 secondToLastPos = Vector3.zero;
            bezierLength = 0;
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
            Gizmos.DrawLine(controlPoints[1].position, controlPoints[2].position);

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
            Gizmos.DrawLine(secondToLastPos, controlPoints[2].position);

            
        }
    }
}


public static class ExtensionMethods
{

    public static Vector3 GetPositionFromCompletionPercentage(this QuadraticBezier main, float percentageAlongBezier)
    {
        
        Vector3 p0 = main.controlPoints[0].position;
        Vector3 p1 = main.controlPoints[1].position;
        Vector3 p2 = main.controlPoints[2].position;
        float t = percentageAlongBezier;
        if (t < 0)
        {
            return p1;
        }
        else if (t > 1)
        {
            return p2;
        }
        else
        {
            Vector3 bezierCalc = p1 + (Mathf.Pow(1 - t, 2) * (p0 - p1)) + (Mathf.Pow(t, 2) * (p2 - p1));
            return bezierCalc;
        }

        
    }


}