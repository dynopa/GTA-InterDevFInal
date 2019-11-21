using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]private int score = 0;
    public static ScoreManager Instance; 
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Increases the score by 1.
    /// </summary>
    public void IncreaseScore()
    {
        score += 1;
    }

    /// <summary>
    /// Increases the score by the given amount.
    /// </summary>
    /// <param name="scoreIncrement">The amount the score will increase.</param>
    public void IncreaseScore (int scoreIncrement)
    {
        score += scoreIncrement;
    }
}
