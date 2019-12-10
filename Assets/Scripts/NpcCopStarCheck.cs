using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCopStarCheck : MonoBehaviour
{
    [SerializeField] private float distanceCheck = 12.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CheckDistance ()
    {
        if (Vector3.Distance(this.transform.position,PlayerManager.Instance.gameObject.transform.position) < distanceCheck)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
