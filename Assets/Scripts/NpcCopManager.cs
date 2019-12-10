using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCopManager : MonoBehaviour
{

    public int maxNumberOfNpcCops;

    [SerializeField] private List<GameObject> allNpcCops = new List<GameObject>();

    public static int starLevel = 0;

    public int copsKilled = 0;

    public int civsKilled = 0;

    public int carsStolen = 0;

    public int shotsNearCop = 0;


    [SerializeField] private int starScore = 0;

    public static NpcCopManager Instance;

    private float starTimer = 0;

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
        if (RoomForMoreNpcCops(Random.Range(1, maxNumberOfNpcCops)))
        {
            NpcCopSpawn.Instance.SpawnAtCoord();
        }

        if (starScore > 400)
        {
            starLevel = 3;
        } else if (starScore > 180) {

            starLevel = 2;

        } else if (starScore > 35)
        {
            starLevel = 1;
        }
        else
        {
            starLevel = 0;
        }

        starTimer += Time.deltaTime;

        if (starLevel > 0 && starTimer >= .5f / starLevel)
        {
            starScore--;
            starTimer = 0;
            }

        //int copsNearby = 0;

        //foreach (GameObject cop in allNpcCops)
        //{
        //    copsNearby += cop.GetComponent<NpcCopStarCheck>().CheckDistance();
        //}

        //if (copsNearby < 1)
        //{
        //    if (starLevel != 0)
        //    {
        //        starTimer += Time.deltaTime;

        //        if (starTimer >= 30)
        //        {

        //            starLevel--;
        //            starTimer = 0;
        //        }
        //    }
        //}
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

    public void CheckAllCopsForStars()
    {
        int copsNearby = 0;
        foreach(GameObject cop in allNpcCops) {
            copsNearby += cop.GetComponent<NpcCopStarCheck>().CheckDistance();
        }

        if (copsNearby > 0)
        {
            shotsNearCop += copsNearby;
            starScore += 1;
        }

        //if (starLevel == 0)
        //{
        //    if (copsNearby > 0)
        //    {
        //        starLevel++;
        //    }
        //}
    }

    public void CopDeath()
    {
        copsKilled++;
        //copsKilledTemp++;
        CheckAllCopsForStars();

        starScore += 25;

        //if (civsKilledTemp > 15 && copsKilledTemp > 3 && starLevel < 3)
        //{
        //    starLevel++;
        //}
    }

    public void CivDeath()
    {
        civsKilled++;
        //civsKilledTemp++;
        CheckAllCopsForStars();

        starScore += 15;

        //if (civsKilledTemp > 5 && starLevel < 2)
        //{
        //    starLevel++;
        //}
    }

    public void CarStolen ()
    {
        carsStolen++;
        CheckAllCopsForStars();

        starScore += 8;
    }

    public void IncreaseStarScore (int scoreToIncrease)
    {
        starScore += scoreToIncrease;
    }

    public int GetStarLevel()
    {
        return starLevel;
    }
}
