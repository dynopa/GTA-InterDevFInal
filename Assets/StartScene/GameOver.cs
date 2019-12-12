using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour

{
    public Text gameOverText;

    void Update()
    {
        string stringToPrint = "";
        stringToPrint += "Misdemeanors: \n" + NpcCopManager.Instance.copsKilled.ToString() + " Counts of Vandalism";
        stringToPrint += "Felonies: \n" + NpcCopManager.Instance.civsKilled.ToString() + " Counts of Manslaughter";
        stringToPrint += "\n" + NpcCopManager.Instance.carsStolen.ToString() + " Counts of Grand Theft Auto";
        stringToPrint += "Capital Felonies: \n" + NpcCopManager.Instance.copsKilled.ToString() + " Counts of Police Murder";
        stringToPrint += "\nYou Scored: " + ScoreManager.Instance.GetScore();



         gameOverText.text = stringToPrint;
    }
}