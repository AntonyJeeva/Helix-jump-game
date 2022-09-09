using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int bestScore;
    public int score;

    

    public int currentStage = 0;

    public static GameManager singleton;


    
    void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        bestScore = PlayerPrefs.GetInt("Highscore");

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<PlayerController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("next level is called");
    }

    public void RestartLevel() 
    {
        Debug.Log("Game Over");

        singleton.score = 0;

        FindObjectOfType<PlayerController>().ResetBall();

        FindObjectOfType<HelixController>().LoadStage(currentStage);


    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Highscore", score); 
        }

        
    }

   

}


