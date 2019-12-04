using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoplightChange : MonoBehaviour
{
    public int stoplightIndex;

    public Light[] trafficLights;
    public Color[] trafficColors;

    Vector3[] initPositions = new Vector3[3];
    int currentLightDisplay;

    private void Start()
    {
        SetTrafficLight();
        currentLightDisplay = StoplightManager.currentLight;

        for (int i = 0; i < trafficLights.Length; i++)
        {
            initPositions[i] = trafficLights[i].gameObject.transform.localPosition;
        }

        Destroy(trafficLights[1].gameObject);
        Destroy(trafficLights[2].gameObject);
    }

    void Update()
    {
        if (Time.frameCount % 5 == 0) //Checks every 5 frames to optimize
        {
            SetTrafficLight();
        }
    }

    void SetTrafficLight()
    {
        if (StoplightManager.currentLight == 4 && currentLightDisplay == 2)
        {
            ShowYellowLight();
            currentLightDisplay = 1;
        }

        

        if ((stoplightIndex == 1 && StoplightManager.currentLight == 2) || (stoplightIndex == 2 && StoplightManager.currentLight == 1) || StoplightManager.currentLight == 0)
        {
            currentLightDisplay = 0;
            ShowRedLight();
        }

        if (stoplightIndex == StoplightManager.currentLight)
        {
            currentLightDisplay = 2;
            ShowGreenLight();
        }
    }




    void ShowRedLight()
    {
        trafficLights[0].transform.localPosition = initPositions[0];
        trafficLights[0].color = trafficColors[0];
    }

    void ShowYellowLight()
    {
        trafficLights[0].transform.localPosition = initPositions[1];
        trafficLights[0].color = trafficColors[1];
    }

    void ShowGreenLight()
    {
        trafficLights[0].transform.localPosition = initPositions[2];
        trafficLights[0].color = trafficColors[2];
    }
}
