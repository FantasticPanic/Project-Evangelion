using System;
using UnityEngine;

public class PlayerShipBuild : MonoBehaviour 
{
	[SerializeField]
	GameObject[] shopButtons;
	GameObject target;
 	GameObject tmpSelection;
	GameObject textBoxPanel;

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
        buyButton = textBoxPanel.transform.Find("BUY ?").gameObject;

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
	
	void TurnOffSelectionHighlights()
	{
		for (int i = 0; i < shopButtons.Length; i++)
		{
			shopButtons[i].SetActive(false);
		}
	}

	void UpdateDescriptionBox()
	{
		textBoxPanel.transform.Find("name").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.iconName;
		textBoxPanel.transform.Find("desc").gameObject.GetComponent<TextMesh>().text = tmpSelection.GetComponentInParent<ShopPiece>().ShopSelection.description;	
	}
	void Select()
	{
		tmpSelection = target.transform.Find("SelectionQuad").gameObject;
		tmpSelection.SetActive(true);
	}
	
	public void AttemptSelection()
	{
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
						UpdateDescriptionBox();

                    //if not already sold
                    if (target.transform.Find("itemText").GetComponent<TextMesh>().text != "SOLD")
                    {
                        //can afford
                        Affordable();

                        //can not afford
                        LackofCredits();
                    }
                    else if (target.transform.Find("itemText").GetComponent<TextMesh>().text == "SOLD")
                    {
                        SoldOut();
                    }
						
				 }
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
		}

 	}

    private void WatchAd()
    {
        throw new NotImplementedException();
    }

    //there are some fucking problems with this method (hardcoded)
    private void StartGame()
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("testLevel");        //playerShip will be carried over to next scene
    }



    private void BuyItem()
    {
        Debug.Log("PURCHASED");
        purchaseMade = true;
        buyButton.SetActive(false);
        tmpSelection.SetActive(false);

        for (int i = 0; i < visualWeapons.Length; i++)
        {
            if (visualWeapons[i].name == tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName)
            {
                //gameobject name has to be the same as iconName 
                visualWeapons[i].SetActive(true);
            }
        }
        UpgradeToShip(tmpSelection.transform.parent.gameObject.GetComponent<ShopPiece>().ShopSelection.iconName);
        bank = bank - tmpSelection.transform.parent.GetComponent<ShopPiece>().ShopSelection.cost;
        bankObj.transform.Find("bankText").GetComponent<TextMesh>().text = bank.ToString();
        tmpSelection.transform.parent.transform.Find("itemText").GetComponent<TextMesh>().text = "SOLD";
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
    }

    private void LackofCredits()
    {
        if (bank < target.transform.GetComponent<ShopPiece>().ShopSelection.cost)
        {
            Debug.Log("CAN'T BUY");
           // buyButton.SetActive(false);
        }
    }

    private void Affordable()
    {
        if (bank >= target.transform.GetComponent<ShopPiece>().ShopSelection.cost)
        {
            Debug.Log("CAN BUY");
            buyButton.SetActive(true);
        }
    }

    void Update()
	{
		AttemptSelection();
      
	}
}