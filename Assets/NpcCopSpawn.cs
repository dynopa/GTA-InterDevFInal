using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCopSpawn : MonoBehaviour
{
    [Header("Assign To Use")]
    public GameObject NPCPrefab;
    public GameObject NPCParent;

    public static NpcCopSpawn Instance;

    [Header("Coordinate Max Values")]
    [SerializeField] float yHeight;
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float zMin;
    [SerializeField] float zMax;

    [Header("Add For Manual Spawn Locations")]
    public List<GameObject> spawnCoordList = new List<GameObject>();


    private List<Vector3> spawnCoordListVectors = new List<Vector3>();

    void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// /Spawns NPC Prefab at every coord in spawnCoordList
    /// </summary>
    public void SpawnAtAllCoords()
    {

        foreach (Vector3 v in spawnCoordListVectors)
        {
            SpawnAtCoord(v);
        }
    }

    /// <summary>
    /// Spawns NPC at coordinate. Chooses a random coordinate present in the spawn coords list. Sets a random personaility to the NPC.
    /// </summary>
    public void SpawnAtCoord()
    {
        //Debug.Log(spawnCoordListVectors[Random.Range(0, spawnCoordListVectors.Count)]);
        SpawnAtCoord(spawnCoordListVectors[Random.Range(0, spawnCoordListVectors.Count)]);

    }
    /// <summary>
    /// Spawns NPC at coordinate. Takes in the coordinate where the NPC should be spawned. Sets a random personality to the NPC.
    /// </summary>
    /// <param name="coords">Coordinates where the NPC will spawn.</param>
    public void SpawnAtCoord(Vector3 coords)
    {
        GameObject newNPC = Instantiate(NPCPrefab, coords, Quaternion.identity);

        newNPC.transform.parent = NPCParent.transform;

        if (CheckForLegalSpawn(newNPC))
        {
            NpcCopManager.Instance.AddNpc(newNPC);
        }
        else
        {
            Destroy(newNPC);
        }

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
        coordToAdd.transform.parent = this.transform;
        coordToAdd.transform.position = new Vector3(xR, yHeight, zR);

        spawnCoordListVectors.Add(coordToAdd.transform.position);
        Destroy(coordToAdd);
    }

    /// <summary>
    /// /Add random coords to spawnCoordList. Default method using random range of 1 to 10
    /// </summary>
    public void AddRandomPoints()
    {
        int numOfPoints = Random.Range(7, 11);

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

    /// <summary>
    /// Checks for legal spawn location of a given NPC. Spawn locations are legal if they are on walkable surfaces.
    /// </summary>
    /// <returns><c>true</c>, if for spawn location was legal, <c>false</c> otherwise.</returns>
    /// <param name="npcToCheck">Npc to check.</param>
    private bool CheckForLegalSpawn(GameObject npcToCheck)
    {
        Ray rayCheck = new Ray(npcToCheck.transform.position, -npcToCheck.transform.up);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(rayCheck, out hit, 5))
        {
            if (hit.collider.gameObject.tag != "Concrete")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

}
