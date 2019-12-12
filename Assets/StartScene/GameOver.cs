using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour

{
    public Text gameOverText;

    void Start()
    {
        string stringToPrint = "";
        stringToPrint += "Misdemeanors: \n" + NpcCopManager.Instance.copsKilled.ToString() + " Counts of Vandalism";
        stringToPrint += "\n" + NpcCopManager.Instance.shotsNearCop.ToString() + " Shots Fired near Police";
        stringToPrint += "\n\nFelonies: \n" + NpcCopManager.Instance.civsKilled.ToString() + " Counts of Manslaughter";
        stringToPrint += "\n" + NpcCopManager.Instance.carsStolen.ToString() + " Counts of Auto Theft";
        stringToPrint += "\n\nCapital Felonies: \n" + NpcCopManager.Instance.copsKilled.ToString() + " Counts of Police Murder";
        stringToPrint += "\n\nYou Scored: " + ScoreManager.Instance.GetScore();



         gameOverText.text = stringToPrint;
    }
}