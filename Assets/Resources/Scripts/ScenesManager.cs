using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScenesManager : MonoBehaviour
{
    Scenes scenes;
    [SerializeField]
    private float gameTimer = 0;
    float[] endLevelTimer = { 5, 5, 10 };
    int currentSceneNumber = 0;
    bool gameEnding = false;

    public enum Scenes
    {
        splashScreen,
        title,
        shop,
        level1,
        level2,
        level3,
        gameOver
    }


    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode additive)
    {
        GetComponent<GameManager>().SetLivesDisplay(GameManager.playerLives);

       /* if (GameObject.Find("score"))
        {
            GameObject.Find("score").GetComponent<TextMeshProUGU>().text = ScoreManager.playerScore.ToString();
        }*/
    }

    public void ResetScene()
    {
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
       
    }

    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }

    //tell player score upon gameover
    public void GameOver()
    {
        SceneManager.LoadScene("gameOver");
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayerScore);
    }

    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }

    void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }


    private void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }

    private void  GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level1:
            case Scenes.level2:
            case Scenes.level3:

                if (gameTimer < endLevelTimer[currentSceneNumber-3])
                {
                    //if level has not been completed
                    gameTimer += Time.deltaTime;
                }
                else
                {
                    //if level is completed
                    if (!gameEnding)
                    {
                        gameEnding = true;
                        if (SceneManager.GetActiveScene().name != "level3")
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().LevelEnds = true;
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().GameCompleted = true;
                        }
                        Invoke("NextLevel", 4);
                    }
                }
                break;
        }
    }
}
