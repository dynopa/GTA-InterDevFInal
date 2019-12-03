using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCopManager : MonoBehaviour
{

    public int maxNumberOfNpcCops;

    [SerializeField] private List<GameObject> allNpcCops = new List<GameObject>();

    public static NpcCopManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        NpcCopSpawn.Instance.AddRandomPoints();
        //NpcCivSpawn.Instance.SetUpTransformList();
        NpcCopSpawn.Instance.SpawnAtAllCoords();
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomForMoreNpcCops(maxNumberOfNpcCops))
            NpcCopSpawn.Instance.SpawnAtCoord();
    }


    /// <summary>
    /// Determines if there is room for more npc civs.
    /// </summary>
    /// <returns><c>true</c>, if there is room for more npc civs, <c>false</c> otherwise.</returns>
    /// <param name="max">Max.</param>
    private bool RoomForMoreNpcCops(int max)
    {
        return allNpcCops.Count <= max;
    }

    /// <summary>
    /// Adds the npc.
    /// </summary>
    /// <param name="NpcToAdd">Npc to add.</param>
    public void AddNpc(GameObject NpcToAdd)
    {
        allNpcCops.Add(NpcToAdd);
    }

    /// <summary>
    /// Removes the npc.
    /// </summary>
    /// <param name="NpcToRemove">Npc to remove.</param>
    public void RemoveNpc(GameObject NpcToRemove)
    {
        allNpcCops.Remove(NpcToRemove);
    }
}
