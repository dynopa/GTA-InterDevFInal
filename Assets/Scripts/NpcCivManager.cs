using System.Collections;
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
        NpcCivSpawn.Instance.SpawnAtAllCoords();
    }

    // Update is called once per frame
    void Update()
    {
        if (RoomForMoreNpcCivs(maxNumberOfNpcCivs))
            NpcCivSpawn.Instance.SpawnAtCoord();
    }


    private bool RoomForMoreNpcCivs(int max)
    {
        return allNpcCivs.Count <= max;
    }

    public void AddNpc(GameObject NpcToAdd)
    {
        allNpcCivs.Add(NpcToAdd);
    }

    public void RemoveNpc(GameObject NpcToRemove)
    {
        allNpcCivs.Remove(NpcToRemove);
    }
}
