using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleComponent : MonoBehaviour
{
    public string loadThisScene;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(loadThisScene);
        }
    }
}
