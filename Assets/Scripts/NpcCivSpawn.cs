using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivSpawn : MonoBehaviour
{
    [Header("Assign To Use")]
    public GameObject NPCPrefab;
    public GameObject NPCParent;

    public static NpcCivSpawn Instance;

    [Header("Coordinate Max Values")]
    [SerializeField] float yHeight;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float zMin;
    [SerializeField] float zMax;

    [Header("Add For Manual Spawn Locations")]
    public List<GameObject> spawnCoordList = new List<GameObject>();

    [Header("PossiblePersonalities")]
    public List<NpcBehaviorPersonality_SC> allPersonalities = new List<NpcBehaviorPersonality_SC>();
  


    void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// /Spawns NPC Prefab at every coord in spawnCoordList
    /// </summary>
    public void SpawnAtAllCoords() {

        foreach (GameObject obj in spawnCoordList)
        {
            SpawnAtCoord(obj);
        }
    }

    /// <summary>
    /// Spawns NPC at coordinate. Takes in the coordinate where the NPC should be spawned. Sets a random personality to the NPC.
    /// </summary>
    /// <param name="coords">Coordinates where the NPC will spawn.</param>
    public void SpawnAtCoord(GameObject coords)
    {
        GameObject newNPC = Instantiate(NPCPrefab, coords.transform.position, Quaternion.identity);
        Destroy(coords);
        newNPC.transform.parent = NPCParent.transform;

        //Set a random personality to the NPC.
        newNPC.GetComponent<NpcCivMoveWalk>().SetPersonality(allPersonalities[(int)Random.Range(0, allPersonalities.Count)]);
    }

    /// <summary>
    /// Adds a random point to the list of coords where NPCs can be spawned.
    /// </summary>
    public void AddRandomPoint()
    {
        float xR = Random.Range(xMin, xMax);
        //float yR = Random.Range(0f, 10f);
        float zR = Random.Range(zMin, zMax);
        GameObject coordToAdd = new GameObject();
        coordToAdd.transform.position = new Vector3(xR, yHeight, zR);

        spawnCoordList.Add(coordToAdd);
    }

    /// <summary>
    /// /Add random coords to spawnCoordList. Default method using random range of 1 to 10
    /// </summary>
    public void AddRandomPoints()
    {
        int numOfPoints = Random.Range(30, 50);

        for (int i = 0; i < numOfPoints; i++)
        {
            AddRandomPoint();
        }

    }

    /// <summary>
    /// /Add random coords to spawnCoordList. Specify lower and upper bounds of random range
    /// </summary>
    public void AddRandomPoints(int lowBound, int highBound)
    {
        int numOfPoints = Random.Range(lowBound, highBound);

        for (int i = 0; i < numOfPoints; i++)
        {
            AddRandomPoint();
        }

    }

}
