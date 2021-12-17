using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipBuild : MonoBehaviour
{
    //[SerializeField]
    //GameObject[] shopButtons;
    GameObject target;
    GameObject tmpSelection;
    GameObject textBoxPanel;
    int number;

    [SerializeField]
    GameObject[] visualWeapons;
    [SerializeField]
    SOActorModel defaultPlayerShip;
    GameObject playerShip;
    GameObject buyButton;
    GameObject bankObj;             //assign bank variable
    int bank = 600;
    bool purchaseMade = false;       //boolean variable for purchasing

    void Start()
    {
        textBoxPanel = GameObject.Find("textBoxPanel");
        TurnOffSelectionHighlights();
        purchaseMade = false;
        bankObj = GameObject.Find("bank");
        bankObj.GetComponentInChildren<TextMesh>().text = bank.ToString();
        buyButton = GameObject.Find("BUY?").gameObject;

        TurnOffPlayerShipVisuals();
        PreparePlayerShipForUpgrade();
    }

    //reset visual of player's ship
    private void PreparePlayerShipForUpgrade()
    {
        playerShip = GameObject.Instantiate(Resources.Load("Prefabs/Player/Player_Ship")) as GameObject;
        playerShip.GetComponent<Player>().enabled = false;
        playerShip.transform.position = new Vector3(0, 10000, 0);
        playerShip.GetComponent<IActorTemplate>().ActorStats(defaultPlayerShip);
    }
    //creates player's ship when it has all upgrades applied
    private void TurnOffPlayerShipVisuals()
    {
        for (int i = 0; i < visualWeapons.Length; i++)
        {
            visualWeapons[i].gameObject.SetActive(false);
        }
    }
    //REMOVED 01
    /*
    GameObject ReturnClickedObject (out RaycastHit hit)
	{
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction * 100, out hit)) 
		{
			target = hit.collider.gameObject;
		}
		return target;
	}
    */

    void TurnOffSelectionHighlights()
    {
        GameObject[] selections = GameObject.FindGameObjectsWithTag("Selection");
        for (int i = 0; i < selections.Length; i++)
        {
            if (selections[i].GetComponentInParent<ShopPiece>())
            {
                if (selections[i].GetComponentInParent<ShopPiece>().ShopSelection.iconName == "sold out")
                {
                    selections[i].SetActive(false);
                }
            }
        }
    }

    void UpdateDescriptionBox()
    {
        //textBoxPanel.transform.Find("name").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.iconName;
        //textBoxPanel.transform.Find("desc").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.description;
        GameObject.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.iconName;
        GameObject.Find("description").gameObject.GetComponent<TextMeshProUGUI>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.description;
    }

    //REMOVED 02
    /*
	void Select()
	{
		tmpSelection = target.transform.Find("SelectionQuad").gameObject;
		tmpSelection.SetActive(true);
	}
    */

    public void AttemptSelection(GameObject buttonName)
    {
        //REMOVED 03
        /*
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hitInfo;
			target = ReturnClickedObject (out hitInfo);

			if (target != null)
			{
            
				if (target.transform.Find("itemText"))
				{
						TurnOffSelectionHighlights();
						Select();
                        */
        if (buttonName)
        {
            TurnOffSelectionHighlights();
            tmpSelection = buttonName;
            tmpSelection.transform.GetChild(1).gameObject.SetActive(true);
        }
        UpdateDescriptionBox();

        //if not already sold
        if (buttonName.GetComponentInChildren<Text>().text != "SOLD")
        {
            //can afford
            Affordable();

            //can not afford
            LackofCredits();
        }
        else if (buttonName.GetComponentInChildren<Text>().text == "SOLD")
        {
            SoldOut();
        }

    }
    //REMOVED 04
    /*
                else if (target.name == "BUY ?")
                {
                    BuyItem();

                }

                else if (target.name == "START")
                {
                    StartGame();
                }

                else if (target.name == "WATCH AD")
                {
                    WatchAd();
                }
			}
            */

    private void WatchAd()
    {
        throw new NotImplementedException();
    }

    public void StartGame()
    {
        if (purchaseMade)
        {
            playerShip.name = "UpgradedShip";                       //rename our ship "UpgradedShip"
            if (playerShip.transform.Find("Armor Piece(Clone)"))      //check if player bought health upgrade 
            {
                playerShip.GetComponent<Player>().Health = 2;
            }
            DontDestroyOnLoad(playerShip);
        }
        GameManager.Instance.GetComponent<ScenesManager>().BeginGame(GameManager.gameLevelScene);      //playerShip will be carried over to next scene
    }



    public void BuyItem()
    {
        Debug.Log("PURCHASED");
        purchaseMade = true;
        buyButton.SetActive(false);
        //tmpSelection.SetActive(false);

        for (int i = 0; i < visualWeapons.Length; i++)
        {
            if (visualWeapons[i].name == tmpSelection.GetComponent<ShopPiece>().ShopSelection.iconName)
            {
                //gameobject name has to be the same as iconName 
                visualWeapons[i].SetActive(true);
            }
        }
        UpgradeToShip(tmpSelection.GetComponent<ShopPiece>().ShopSelection.iconName);
      //  if (tmpSelection.transform.parent.GetComponent<ShopPiece>().ShopSelection.cost == System.Int32.Parse()))
        //{
            bank = bank - System.Int32.Parse(tmpSelection.GetComponent<ShopPiece>().ShopSelection.cost);
        //}
        bankObj.transform.Find("bankText").GetComponent<TextMesh>().text = bank.ToString();
        tmpSelection.transform.Find("itemText").GetComponent<Text>().text = "SOLD";
    }

    void UpgradeToShip(string upgrade)
    {

        GameObject shipItem = GameObject.Instantiate(Resources.Load("Prefabs/Player/" + upgrade)) as GameObject;
        shipItem.transform.SetParent(playerShip.transform);         //set Item parent to player's ship
        shipItem.transform.localPosition = Vector3.zero;            //zero it's local position
    }

    private void SoldOut()
    {
        Debug.Log("SOLD OUT");
        buyButton.SetActive(false);
    }

    private void LackofCredits()
    {
        //  if (int.TryParse(target.transform.GetComponent<ShopPiece>().ShopSelection.cost, out number))
        //{
        
            if (bank < System.Int32.Parse(tmpSelection.GetComponentInChildren<Text>().text))
            {
                Debug.Log("CAN'T BUY");
                buyButton.SetActive(false);
            }
           
        //}
    }

    private void Affordable()
    {
        //  if (int.TryParse(target.transform.GetComponent<ShopPiece>().ShopSelection.cost, out number))
        //{
        
            if (bank >= System.Int32.Parse(tmpSelection.GetComponentInChildren<Text>().text))
            {
                Debug.Log("CAN BUY");
                buyButton.SetActive(true);
        }
        
      //  }
    }

    //REMOVED 05
    /* void Update()
        {
            AttemptSelection();

        }
        */
}