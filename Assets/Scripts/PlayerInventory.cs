using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public InventoryGun[] availableGuns = new InventoryGun[3];
    public int currentGunSelection;

    public void GunSelectLeft()
    {
        if (currentGunSelection == 0)
        {
            currentGunSelection = availableGuns.Length - 1;
            if (currentGunSelection < 0)
            {
                currentGunSelection = 0;
            }
        }
        else
        {
            currentGunSelection--;
        }
    }

    public void GunSelectRight()
    {
        if (currentGunSelection >= availableGuns.Length - 1)
        {
            currentGunSelection = 0;
        }
        else
        {
            currentGunSelection++;
        }
    }

    [System.Serializable]
    public struct InventoryGun
    {
        public Gun gunType;
        public int currentAmmo;
    }

}
