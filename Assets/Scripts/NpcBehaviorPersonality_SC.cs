using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GTA/NPC/Personality")]
public class NpcBehaviorPersonality_SC : ScriptableObject
{
    [Header("Threshold Distance")]
    public float distFromPlayerToFear;

    [Header("Health")]
    public int health;
}
