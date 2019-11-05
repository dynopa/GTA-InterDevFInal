using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCivSpawn : MonoBehaviour
{

    public GameObject NPCPrefab;
    public GameObject NPCParent;
    public List<GameObject> spawnCoordList = new List<GameObject>();

    public static NpcCivSpawn Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        AddRandomPoints();
        SpawnAtAllCoords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// /Spawns NPC Prefab at every coord in spawnCoordList
    /// </summary>
     
    public void SpawnAtAllCoords() {

        foreach (GameObject obj in spawnCoordList)
        {
            SpawnAtCoord(obj.transform.position);
        }
    }

    public void SpawnAtCoord(Vector3 coords)
    {
        GameObject newNPC = Instantiate(NPCPrefab, coords, NPCPrefab.transform.rotation);
        newNPC.transform.parent = NPCParent.transform;
    }

    public void AddRandomPoint()
    {
        float xR = Random.Range(0f, 10f);
        float yR = Random.Range(0f, 10f);
        float zR = Random.Range(0f, 10f);
        GameObject coordToAdd = new GameObject();
        coordToAdd.transform.position = new Vector3(xR, yR, zR);

        spawnCoordList.Add(coordToAdd);
    }

    /// <summary>
    /// /Add random coords to spawnCoordList. Default method using random range of 1 to 10
    /// </summary>
    public void AddRandomPoints()
    {
        int numOfPoints = Random.Range(1, 10);

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
