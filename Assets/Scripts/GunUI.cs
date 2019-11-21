using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{

    public TextMeshProUGUI gunAmmoTxt;
    [Space]
    public TextMeshProUGUI reloadingTxt;
    public Slider reloadingSlider;
    [Space]
    public TextMeshProUGUI gunNameTxt;

    PlayerGun playerGun;

    // Start is called before the first frame update
    void Start()
    {
        playerGun = PlayerManager.Instance.gameObject.GetComponentInChildren<PlayerGun>();
        reloadingSlider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

        string gunText = playerGun.currentAmmo + "/" + playerGun.currentGun.clipSize;
        gunAmmoTxt.text = gunText;

        if (playerGun.timeLeftOfReload > 0)
        {
            reloadingSlider.gameObject.SetActive(true);
            reloadingSlider.value = playerGun.timeLeftOfReload.Remap(0, playerGun.currentGun.reloadTime, 1, 0);

            reloadingTxt.enabled = true;

            gunNameTxt.enabled = false;

        }
        else
        {
            reloadingSlider.gameObject.SetActive(false);
            reloadingTxt.enabled = false;

            gunNameTxt.enabled = true;
            gunNameTxt.text = playerGun.currentGun.gunName;
        }
    }
}
