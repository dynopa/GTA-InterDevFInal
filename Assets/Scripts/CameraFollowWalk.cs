using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWalk : MonoBehaviour
{

    public bool inUse;

    float cameraHeight; //Height offset above player (player.pos.y + cameraHeight = Camera's y position)

    [Header("Camera Easing")]
    public bool useEasing;
    public float dampingValue; //0-1 Value ~ At 1, camera position will stay rigid to player position
    Vector3 lastFramePos; //Camera's position during the last frame 


    void Start()
    {
        cameraHeight = Camera.main.transform.position.y - PlayerManager.position.y;
    }

    void FixedUpdate()
    {
        if (inUse)
        {
            float targetYTemp = Camera.main.transform.position.y;
            Vector3 targetCameraPos = PlayerManager.position + new Vector3(0, cameraHeight, 0);
            if (useEasing)
            {
                
                targetCameraPos = ((targetCameraPos - transform.position) * dampingValue) + transform.position;
                
            }

            targetCameraPos.y = targetYTemp;
            Camera.main.transform.position = targetCameraPos;
        }
    }
}
