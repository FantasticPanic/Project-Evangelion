using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour
{

    public string loadThisScene;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GetComponentInChildren<ScoreManager>().ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3)
        {
            SceneManager.LoadScene(loadThisScene);
        }
    }
}
