using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GTA/NPC/Emotion")]
public class NpcBehaviorEmotion_SC : ScriptableObject
{
    [Header("Distances")]
    public float distFromPlayer;

    [Header("Speeds")]
    public float speed;
}
