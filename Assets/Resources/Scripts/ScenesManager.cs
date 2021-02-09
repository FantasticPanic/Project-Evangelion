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
    }

    public void GameOver()
    {
        SceneManager.LoadScene("gameOver");
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("testLevel");
    }

    
}
