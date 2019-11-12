using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    //In each gun, you can set the statistics/audio of the bullet it spawns


    public string gunName;
    public Mesh gunObj;
    [Space]
    [Header("Bullet Fire")]
    public GameObject bulletType;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletForce; //Force multiplier when hitting movable objects
    [Space]
    [Header("Gun Stats")]
    public int clipSize;
    [Space]
    public bool fullyAuto;
    public float fireRate;
    public float reloadTime;
    [Space]
    public float bloomAmount;
    [Space]
    [Header("Explosives")]
    public bool explosive;
    public float explosionRadius;
    public float upForce;
    [Header("Graphical")]
    public float recoilAmount;
    public Texture2D gunTextureUI;
    [Header("Audio")]
    public AudioClip gunshotClip;
    public AudioClip bulletZoomClip;



}
