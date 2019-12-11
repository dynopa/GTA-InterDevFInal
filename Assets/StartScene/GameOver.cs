using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour

{
    public Text gameOverText;

    void Update()
    {
        gameOverText.text = "You Scored: " + ScoreManager.Instance.scoreText.text;
    }
}