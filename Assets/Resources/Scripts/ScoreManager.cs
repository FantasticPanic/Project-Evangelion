using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public static int playerScore;
    public int PlayerScore

    {
        get
        {
            return playerScore;
        }
    }

    public void SetScore(int incomingScore)
    {
        playerScore += incomingScore;

    }

    private void Update()
    {
        GameObject.Find("score").GetComponent<TextMeshProUGUI>().text = playerScore.ToString();
    }
    public void ResetScore()
    {
        playerScore = 0;
        if (GameObject.Find("score"))
        {
           
            GameObject.Find("score").GetComponent<TextMeshProUGUI>().text = playerScore.ToString();

        }
    }
}
