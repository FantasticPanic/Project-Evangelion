using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransition : MonoBehaviour
{

    Vector3 transitionToEnd = new Vector3(-100, 0, 0);
    Vector3 transtiionToCompleteGame = new Vector3(7000, 0, 0);
    Vector3 readyPos = new Vector3(900, 0, 0);
    Vector3 startPos;

    float distCovered;               // hold time data to measure 2 Vector3 points
    float journeyLength;             //hold distance between startPos and readyPos

    bool levelStarted = true;
    bool speedOff = false;
    bool levelEnds = false;
    bool gameCompleted = false;

    public bool LevelEnds
    {
        get { return levelEnds; }
        set { levelEnds = value; }
    }

    public bool GameCompleted
    {
        get { return gameCompleted; }
        set { gameCompleted = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Vector3.zero;
        startPos = transform.position;
        Distance();
    }

    private void Distance()
    {
        journeyLength = Vector3.Distance(startPos, readyPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelStarted)
        {
            StartCoroutine(PlayerMovement(transitionToEnd, 10));
        }
    }

    void SpeedOff()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 800);
    }

    IEnumerator PlayerMovement(Vector3 point, float transitionSpeed)
    {
        //check if the player is in the correct position before running the rest of the code
        if (Mathf.Round(transform.localPosition.x) >= readyPos.x - 5 &&
            Mathf.Round(transform.localPosition.x) <= readyPos.x + 5 &&
            Mathf.Round(transform.localPosition.y) >= -5f &&
            Mathf.Round(transform.localPosition.y) <= +5f)
        {
            if (levelEnds)
            {
                levelEnds = false;
                speedOff = true;
            }
            if (levelStarted)
            {
                levelStarted = false;
                distCovered = 0;
                GetComponent<Player>().enabled = true;
            }
            yield return null;
        }
        else
        {
            distCovered += Time.deltaTime * transitionSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, point, fractionOfJourney);
        }
    }
}
