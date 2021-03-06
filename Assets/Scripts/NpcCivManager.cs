﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivManager : MonoBehaviour
{

    public int maxNumberOfNpcCivs;

    [SerializeField] private List<GameObject> allNpcCivs = new List<GameObject>();

    public static NpcCivManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        NpcCivSpawn.Instance.AddRandomPoints();
       //NpcCivSpawn.Instance.SetUpTransformList();
        NpcCivSpawn.Instance.SpawnAtAllCoords();
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomForMoreNpcCivs(maxNumberOfNpcCivs))
            NpcCivSpawn.Instance.SpawnAtCoord();
    }


    /// <summary>
    /// Determines if there is room for more npc civs.
    /// </summary>
    /// <returns><c>true</c>, if there is room for more npc civs, <c>false</c> otherwise.</returns>
    /// <param name="max">Max.</param>
    private bool RoomForMoreNpcCivs(int max)
    {
        return allNpcCivs.Count <= max;
    }

    /// <summary>
    /// Adds the npc.
    /// </summary>
    /// <param name="NpcToAdd">Npc to add.</param>
    public void AddNpc(GameObject NpcToAdd)
    {
        allNpcCivs.Add(NpcToAdd);
    }

    /// <summary>
    /// Removes the npc.
    /// </summary>
    /// <param name="NpcToRemove">Npc to remove.</param>
    public void RemoveNpc(GameObject NpcToRemove)
    {
        allNpcCivs.Remove(NpcToRemove);
    }

    public void CheckAllNpcsForScared()
    {
        foreach (GameObject npc in allNpcCivs)
        {
            npc.GetComponent<NpcCivPersonalityManager>().CheckForScared();
        }
    }
}
