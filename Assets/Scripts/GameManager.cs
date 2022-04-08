using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int currentScore;
    public int currentLevel;
    public static GameManager singleton;

    void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("BestScore"); // update the best score according to the saved data
    }

    public void NextLevel()
    {
        Debug.Log("Next level");
    }

    public void RestartLevel()
    {
        Debug.Log("Restert level");
        singleton.currentScore = 0;
        FindObjectOfType<BallController>().ResetBallPosition();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", currentScore); // save the best score
        }
    }
}
