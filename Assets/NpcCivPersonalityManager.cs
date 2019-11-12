using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivPersonalityManager : MonoBehaviour
{
    //[Header("Tuning")]
    //[SerializeField] float walkSpeed = 10f;
    [Header("Personality")]
    public NpcBehaviorPersonality_SC personality;

    [Header("Current Emotion")]
    public NpcBehaviorEmotion_SC currentEmotion;

    [Header("Possible Emotions")]
    public NpcBehaviorEmotion_SC normal;
    public NpcBehaviorEmotion_SC frightened;

    [Header("PossiblePersonalities")]
    public List<NpcBehaviorPersonality_SC> allPersonalities = new List<NpcBehaviorPersonality_SC>();

    void Awake()
    {
        currentEmotion = normal;
    }

    /// <summary>
    /// Sets the personality of this NPC.
    /// </summary>
    /// <param name="p">The personality to set.</param>
    public void SetPersonality(NpcBehaviorPersonality_SC p)
    {
        this.personality = p;
    }

    public void SetPersonality()
    {
        //Set a random personality to the NPC.
        SetPersonality(allPersonalities[(int)Random.Range(0, allPersonalities.Count)]);
    }
}
