using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    SOActorModel actorModel;
    GameObject playerShip;
    bool upgradeShip = false;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        GetComponentInChildren<Player>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreatePlayer()
    {
       // actorModel = Object.Instantiate(Resources.Load("Scripts/ScriptableObjects/Player_Default")) as SOActorModel;
       // playerShip = GameObject.Instantiate(actorModel.actor) as GameObject;
       // playerShip.GetComponent<Player>().ActorStats(actorModel);

     

        //Player has been shopping
        if (GameObject.Find("UpgradedShip"))
        {
            upgradeShip = true;
        }

        //Didn't shop or died
        //default ship build
        if (!upgradeShip || GameManager.Instance.Died)
        {

            GameManager.Instance.Died = false;
            actorModel = Object.Instantiate(Resources.Load("Scripts/ScriptableObjects/Player_Default")) as SOActorModel;
            playerShip = GameObject.Instantiate(actorModel.actor, this.transform.position, Quaternion.Euler(270, 180, 0)) as GameObject;

            playerShip.GetComponent<IActorTemplate>().ActorStats(actorModel);
        }
        else
        {
            playerShip = GameObject.Find("UpgradedShip");
        }
        
        //set up player
        playerShip.transform.rotation = Quaternion.Euler(0, 90, 0);
        playerShip.transform.localScale = new Vector3(3, 3, 3);
        playerShip.name = "Player";
        playerShip.transform.SetParent(this.transform);
        playerShip.transform.position = Vector3.zero;
    }
}
