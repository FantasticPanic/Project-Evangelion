using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ScenesManager : MonoBehaviour
{
    Scenes scenes;


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

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.GetComponent<ScoreManager>().ResetScore();
    }

    //tell player score upon gameover
    public void GameOver()
    {
        SceneManager.LoadScene("gameOver");
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayerScore);
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("testLevel");
    }

    
}
