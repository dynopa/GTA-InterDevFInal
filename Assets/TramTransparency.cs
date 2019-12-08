using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramTransparency : MonoBehaviour
{
    Material origMat;
    // Start is called before the first frame update
    void Start()
    {
        origMat = this.GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, this.transform.position) < 25f)
       {
            Material tempMat = this.GetComponentInChildren<MeshRenderer>().material;
            Color tempColor = tempMat.color;
            tempColor.a = .5f;
            tempMat.color = tempColor;
            this.GetComponentInChildren<MeshRenderer>().material = tempMat;
        }
        else
        {
            this.GetComponentInChildren<MeshRenderer>().material = origMat;
        }
    }
}

