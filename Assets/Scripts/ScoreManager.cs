using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score = 0;
    public static ScoreManager Instance; 
    public Text scoreText;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
   
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (Input.GetKey(KeyCode.P)){
            SceneManager.LoadScene(2);
        }
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

    public string GetScore () {
        return this.score.ToString();
    }
}
