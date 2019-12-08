using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class starDisplay : MonoBehaviour
{
    public int starNumber;
    public Image starImage;

    // Update is called once per frame
    void Update()
    {
        if (NpcCopManager.starLevel >= starNumber)
        {
            starImage.enabled = true;
        }
        else starImage.enabled = false;
    }
}
