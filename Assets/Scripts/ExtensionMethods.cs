using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    /// <summary>
    /// Returns the world position of "t" percentage along a given bezier.
    /// </summary>
    /// <param name="checkedBezier"></param>
    /// <param name="percentageAlongBezier"></param>
    /// <returns></returns>
    public static Vector3 GetPositionFromCompletionPercentage(this QuadraticBezier main, float t)
    {

        Vector3 p0 = main.points[0];
        Vector3 p1 = main.points[1];
        Vector3 p2 = main.points[2];
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
