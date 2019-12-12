using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHealth : MonoBehaviour
{
    public PlayerDeath pDeath;
    public AnimationCurve healthDisplayIncrease;
    public Image bloodDisplay;

    float alphaValue;
    float expectedAlpha;




    void Update()
    {
        print(pDeath.health);
        expectedAlpha = (float)pDeath.health/60;
        float actualAlpha = healthDisplayIncrease.Evaluate(expectedAlpha);

        bloodDisplay.color = new Color(1, 1, 1, actualAlpha);
    }


}
