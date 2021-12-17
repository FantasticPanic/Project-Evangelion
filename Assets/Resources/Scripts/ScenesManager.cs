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
    public AudioClip[] levelSongs;

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


   // public MusicMode musicMode;
    public enum MusicMode
    {
        noSound,
        fadeDown,
        musicOn,
    }


    private void OnSceneLoaded(Scene ascene, LoadSceneMode aMode)
    {
        StartCoroutine(MusicVolume(MusicMode.musicOn));
        GetComponent<GameManager>().SetLivesDisplay(GameManager.playerLives);

        if (GameObject.Find("score"))
        {
            GameObject.Find("score").GetComponent<TextMeshProUGUI>().text = ScoreManager.playerScore.ToString();
        }
    }

    private void Start()
    {
        StartCoroutine(MusicVolume(MusicMode.musicOn));
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    //tell player score upon gameover
    public void GameOver()
    {
        SceneManager.LoadScene("gameOver");
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayerScore);
    }



    void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();


        }
        GameTimer();
        LevelMusic();
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
             
                //check if audio clip in our Audio Source is empty
                if (GetComponentInChildren<AudioSource>().clip == null)
                {
                    //AudioClip lvlMusic = Resources.Load<AudioClip>("Sounds/Abstraction - Three Red Hearts - Box Jump") as AudioClip;
                    //GetComponentInChildren<AudioSource>().clip = lvlMusic;
                    GetComponentInChildren<AudioSource>().Play();

                }

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
                        StartCoroutine(MusicVolume(MusicMode.fadeDown));
                        Invoke("NextLevel", 4);
                    }
                }
                //check if audio clip in our Audio Source is empty
                break;
                }
    }

    private void LevelMusic()
    {
        switch (scenes)
        {
            case Scenes.level1:
                {
                    GetComponentInChildren<AudioSource>().clip = levelSongs[0];
                    break;
                }
            case Scenes.level2:
                {
                    GetComponentInChildren<AudioSource>().clip = levelSongs[1];
                    break;
                }

            case Scenes.level3:
                {
                    GetComponentInChildren<AudioSource>().clip = levelSongs[2];
                }
                break;
        }
    }

    IEnumerator MusicVolume(MusicMode musicMode)
    {
        switch (musicMode)
        {
            case MusicMode.fadeDown:
                {
                    GetComponentInChildren<AudioSource>().volume -= Time.deltaTime * 3;
                    break;
                }
            case MusicMode.noSound:
                {
                    GetComponentInChildren<AudioSource>().Stop();
                    break;
                }
            case MusicMode.musicOn:
                {
                    if (GetComponentInChildren<AudioSource>().clip != null)
                    {
                        GetComponentInChildren<AudioSource>().Play();
                        GetComponentInChildren<AudioSource>().volume = 1;
                    }
                    break;
              }     
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void ResetScene()
    {
        StartCoroutine(MusicVolume(MusicMode.noSound));
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);

    }

    void NextLevel()
    {
        StartCoroutine(MusicVolume(MusicMode.musicOn));
        gameEnding = false;
        gameTimer = 0;
        if (GameManager.playerLives < 3)
        {
            GameManager.playerLives = 3;
        }
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }

    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }

}
