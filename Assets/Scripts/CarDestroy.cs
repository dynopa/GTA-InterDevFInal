using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroy : MonoBehaviour
{
    CarAlignToBezier carAlignScript;
    CarFollowBezier carFollowScript;
    Rigidbody rb;

    public float waitTilDestroyTime;
    float timeTilDestroy;

    Vector3 positionToMoveFrom;
    public float changedDistanceToReset; //How far the car needs to move from "positionToMoveFrom" in order to reset the timer and allow it to continue moving

    CarAlignVariables initAlignScriptVariables;
    CarFollowVariables initFollowScriptVariables;
    RigidBodyVariables initRigidbodyVariables;

    [HideInInspector] public Vector3 initPosition;
    [HideInInspector] public Quaternion initRotation;

    public bool playerEnteredCar = false;

    private void Awake()
    {
        carAlignScript = GetComponent<CarAlignToBezier>();
        carFollowScript = GetComponent<CarFollowBezier>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        

        initPosition = rb.transform.position;
        initRotation = rb.transform.rotation;
        StoreScriptVariables();
        
    }

    private void Update()
    {
        if ((transform.position - positionToMoveFrom).magnitude < changedDistanceToReset)
        {
            timeTilDestroy -= Time.deltaTime;
        }
        else
        {
            timeTilDestroy = waitTilDestroyTime;
            positionToMoveFrom = transform.position;
        }
        

        if (timeTilDestroy < 0 && Vector3.Distance(this.transform.position, PlayerManager.Instance.transform.position) > 50 && Vector3.Distance(initPosition, PlayerManager.Instance.transform.position) > 50)
        {
            ReplaceCarScripts();
            carFollowScript.ResetPosition(initPosition, initRotation);
        }
    }


    public void ReplaceCarScripts()
    {
        if (carAlignScript == null)
        {
            carAlignScript = this.gameObject.AddComponent<CarAlignToBezier>();
            SetAlignScriptToInit();
        }
        if (carFollowScript == null)
        {
            carFollowScript = this.gameObject.AddComponent<CarFollowBezier>();
            SetFollowScriptToInit();
        }

        if (rb == null)
        {
            rb = this.gameObject.AddComponent<Rigidbody>();
            SetRigidBodyVariables();
        }
    }



    public void SetAlignScriptToInit()
    {
        if (carAlignScript != null)
        {
            carAlignScript.tOffset = initAlignScriptVariables.tOffset;
            carAlignScript.maxRotationSpeed = initAlignScriptVariables.rotationAlignSpeed;
        }
    }

    public void SetFollowScriptToInit()
    {
        if (carFollowScript != null)
        {
            carFollowScript.speed = initFollowScriptVariables.speed;
            carFollowScript.changePathSpeed = initFollowScriptVariables.changePathSpeed;

            carFollowScript.frontSensorOffset = initFollowScriptVariables.frontSensorOffset;
            carFollowScript.sensorWidth = initFollowScriptVariables.sensorWidth;
            carFollowScript.sensorDistance = initFollowScriptVariables.sensorDistance;

        }
    }

    public void SetRigidBodyVariables()
    {
        if (rb != null)
        {
            rb.mass = initRigidbodyVariables.mass;
            rb.drag = initRigidbodyVariables.drag;
            rb.angularDrag = initRigidbodyVariables.angularDrag;

            rb.isKinematic = initRigidbodyVariables.isKinematic;
            rb.useGravity = false;

            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void StoreScriptVariables()
    {
        if (carAlignScript != null)
        {
            initAlignScriptVariables.tOffset = carAlignScript.tOffset;
            initAlignScriptVariables.rotationAlignSpeed = carAlignScript.maxRotationSpeed;
        }

        if (carFollowScript != null)
        {
            initFollowScriptVariables.speed = carFollowScript.speed;
            initFollowScriptVariables.changePathSpeed = carFollowScript.changePathSpeed;

            initFollowScriptVariables.frontSensorOffset = carFollowScript.frontSensorOffset;
            initFollowScriptVariables.sensorWidth = carFollowScript.sensorWidth;
            initFollowScriptVariables.sensorDistance = carFollowScript.sensorDistance;
        }

        if (rb != null)
        {
            initRigidbodyVariables.mass = rb.mass;
            initRigidbodyVariables.drag = rb.drag;
            initRigidbodyVariables.angularDrag = rb.angularDrag;

            initRigidbodyVariables.isKinematic = rb.isKinematic;
        }
    }
}

public struct CarFollowVariables
{
    public float speed;
    public float changePathSpeed;

    public float frontSensorOffset;
    public float sensorWidth;
    public float sensorDistance;
}

public struct CarAlignVariables
{
    public float tOffset;
    public float rotationAlignSpeed;
}

public struct RigidBodyVariables
{
    public float mass;
    public float drag;
    public float angularDrag;

    public bool isKinematic;
}
