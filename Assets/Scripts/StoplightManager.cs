using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoplightManager : MonoBehaviour
{
    public static int currentLight; //0 = All STOP // 1 = Vertical has Greeen // 2 = Horizontal has Green // 4 = Yellow Light
    public int currentLightDisplay;

    public float lightTime; //Time during which one lane has a light.
    public float inBetweenTime; //Extra time in between stoplight changes where no lanes can go.


    private void Awake()
    {
        StartCoroutine(LightSwitcher());
    }

    private void Update()
    {
        currentLightDisplay = currentLight;
    }

    public IEnumerator LightSwitcher()
    {
        while (true)
        {
            currentLight = 1;
            yield return new WaitForSeconds(lightTime);
            currentLight = 4;
            yield return new WaitForSeconds(inBetweenTime/2);
            currentLight = 0;
            yield return new WaitForSeconds(inBetweenTime / 2);
            currentLight = 2;
            yield return new WaitForSeconds(lightTime);
            currentLight = 4;
            yield return new WaitForSeconds(inBetweenTime/2);
            currentLight = 0;
            yield return new WaitForSeconds(inBetweenTime / 2);
            StartCoroutine(LightSwitcher());
        }
    }
}
