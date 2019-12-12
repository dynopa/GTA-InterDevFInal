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
        expectedAlpha = (float)pDeath.health/45;
        alphaValue = alphaValue + (.1f * (healthDisplayIncrease.Evaluate(expectedAlpha) - alphaValue));

        bloodDisplay.color = new Color(1, 1, 1, alphaValue);
    }


}
