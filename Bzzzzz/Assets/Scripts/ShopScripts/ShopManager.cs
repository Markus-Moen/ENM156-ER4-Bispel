using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
	public GameObject shop;
    public TMP_Text honeyText;          // Dispayed honey in-game.
    public ShopItemSO[] shopItemsSO;    // Array of all scriptable objects.
    public ShopTemplate[] shopPanels;   // Array of the used shop panels.
    public GameObject[] shopPanelsGO;   // Array used to handle which of the items in the SO-array that should be active.
    public Button[] myPurchaseBtns;     // Array of all purchase buttons.
    private bool ownFlowHive = false;
    private int buyBeeAmount = 1;
    public TMP_Text buyBeeText;
    public Button incBeeAmountBtn;
    public Button decBeeAmountBtn;


    // Start is called before the first frame update
    void Start()
    {
        // Only activates the shop panels that are being used  
        // since there can be more scriptable objects than panels.
        for (int i = 0; i < shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);        
        honeyText.text = "Honey: " + GameManager.instance.getPlayerHoney().ToString(); 
        shopItemsSO[3].baseCost = 20;
        LoadItems();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
        shopPanels[0].quantityTxt.text = "Owned: " + GameManager.instance.getPlayerBees();
        shopPanels[1].quantityTxt.text = "Owned: " + GameManager.instance.getPlayerFood();
        shopPanels[3].quantityTxt.text = "Owned: " + GameManager.instance.getPlayerHives();
        if (GameManager.instance.getPlayerFood()>= GameManager.instance.getMaxFood()){
    		myPurchaseBtns[1].interactable = false;
    	}
    	if (GameManager.instance.getPlayerBees()>= GameManager.instance.getMaxBees()){
    		myPurchaseBtns[0].interactable = false;
        }
        CheckPurchaseable();
        if (buyBeeAmount <= 1)
            decBeeAmountBtn.interactable = false;
        else
            decBeeAmountBtn.interactable = true;

    }

    //Updates the text elements in each panel.
    public void LoadItems()
    {
        for (int i=0; i<shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].costTxt.text = "Price: " + shopItemsSO[i].baseCost.ToString();
            shopPanels[i].quantityTxt.text = "Owned: " + shopItemsSO[i].owned.ToString();
        }
        shopPanels[2].quantityTxt.text = "";
        shopPanels[4].quantityTxt.text = "";
        shopPanels[5].quantityTxt.text = "";
        shopPanels[6].quantityTxt.text = "";
        shopPanels[7].quantityTxt.text = "";
        shopPanels[8].quantityTxt.text = "";
        buyBeeText.text = "Purchase: " + buyBeeAmount;

    }

    //Updates whether wach button should be enabled or not. 
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (GameManager.instance.getPlayerHoney() >= shopItemsSO[i].baseCost)
            {
                if (i == 0 && GameManager.instance.getPlayerHoney() < shopItemsSO[i].baseCost * buyBeeAmount)
                    myPurchaseBtns[i].interactable = false;
                else if (i == 6 && GameManager.instance.getPlayerHives() == 0)
                {
                    myPurchaseBtns[i].interactable = false;
                }
                else
                {
                    myPurchaseBtns[i].interactable = true;
                }
                /*else if (!ownFlowHive){
                    myPurchaseBtns[i].interactable = true;
                    }*/
            }
            else
                myPurchaseBtns[i].interactable = false;
            honeyText.text = "Honey: " + GameManager.instance.getPlayerHoney().ToString();
        }
        //myPurchaseBtns[6].interactable = !ownFlowHive;
    }

    // Checks if the player's current honey is enough to buy an item. 
    // It it is, the player's owned honey is updated aswell as the 
    // number of owned quantity of the purchased item. 
   /* public void buyItem(int btn)
    {
         if (ownedHoney >= shopItemsSO[btn].baseCost)
        {
            ownedHoney = ownedHoney - shopItemsSO[btn].baseCost;
            GameManager.instance.decPlayerHoney(shopItemsSO[btn].baseCost);
            shopItemsSO[btn].owned += 1;
            shopPanels[btn].quantityTxt.text = "Owned: " + shopItemsSO[btn].owned.ToString();
            CheckPurchaseable();
        }
    }
   */
    public void closeShop(){
    	shop.SetActive(false);
    }
    public void openShop()
    {
        shop.SetActive(true);
        honeyText.text = "Honey: " + GameManager.instance.getPlayerHoney();

        // Medicine costs 80% of what you got when you open the shop. Might want to change this.
        shopItemsSO[4].baseCost = (int)Mathf.Floor(GameManager.instance.getPlayerHoney() * 0.8f);
        shopPanels[4].costTxt.text = "Price: " + shopItemsSO[4].baseCost.ToString();

    }

    public void buyBee()
    {
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[0].baseCost * buyBeeAmount)
        {
            GameManager.instance.incPlayerBees(buyBeeAmount);
            GameManager.instance.decPlayerHoney(shopItemsSO[0].baseCost * buyBeeAmount);
            shopItemsSO[0].owned = GameManager.instance.getPlayerBees();    
            CheckPurchaseable();
        }
    }
    public void buyFood()
    {
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[1].baseCost)
        {
            GameManager.instance.incPlayerFood(1);
            GameManager.instance.decPlayerHoney(shopItemsSO[1].baseCost);
            shopItemsSO[1].owned = GameManager.instance.getPlayerFood();
            CheckPurchaseable();
        }
    }

    public void buyChanceCard(){
    if (GameManager.instance.getPlayerHoney() >= shopItemsSO[2].baseCost)
        {
            GameManager.instance.decPlayerHoney(shopItemsSO[2].baseCost);
            shopItemsSO[2].owned += shopItemsSO[2].owned;
            CheckPurchaseable();
        }
    }

    public void buyInfoCard()
    {
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[5].baseCost)
        {
            GameManager.instance.decPlayerHoney(shopItemsSO[2].baseCost);
            shopItemsSO[5].owned += shopItemsSO[5].owned;
            CheckPurchaseable();
        }
    }

    public void buyNewHive(){
    	if (GameManager.instance.getPlayerHoney() >= shopItemsSO[3].baseCost)
    	{
    		GameManager.instance.decPlayerHoney(shopItemsSO[3].baseCost);
            GameManager.instance.incPlayerHives(1);
            shopItemsSO[3].baseCost = shopItemsSO[3].baseCost * 5;
            shopPanels[3].costTxt.text = "Price: " + shopItemsSO[3].baseCost.ToString();
            CheckPurchaseable();
    	}
    }

    public void buyMedicine(){
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[4].baseCost)
        {
            GameManager.instance.decPlayerHoney(shopItemsSO[4].baseCost);
            Parasites.instance.stopParasites();
            CheckPurchaseable();
        }
    }
    public void buyFlowHive()
    {
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[6].baseCost && GameManager.instance.getPlayerHives() != 0)
        {
            bool b = GameManager.instance.upgradeHive();
            if(b){
                GameManager.instance.decPlayerHoney(shopItemsSO[6].baseCost);
            }
            
            
            //myPurchaseBtns[6].interactable = false;
            //ownFlowHive = true;
            //shopPanels[6].quantityTxt.text = "Flow Hive owned!";
            CheckPurchaseable();
        }
    }



    public void buyHoneyStorageUpgrade(){
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[7].baseCost){      // TODO: set so player cannot buy more than a certain number of upgrades
            GameManager.instance.decPlayerHoney(shopItemsSO[7].baseCost);           // TODO: increase price every time upgrade is bought
            int currentMaxHoney = GameManager.instance.getMaxHoney();
            GameManager.instance.setMaxHoney(currentMaxHoney + 500);    // increase the storage size by 500 honey

            CheckPurchaseable();
        }
    }

    public void buyFoodStorageUpgrade(){
        if (GameManager.instance.getPlayerHoney() >= shopItemsSO[8].baseCost){      // TODO: set so player cannot buy more than a certain number of upgrades
            GameManager.instance.decPlayerHoney(shopItemsSO[8].baseCost);           // TODO: increase price every time upgrade is bought
            int currentMaxFood = GameManager.instance.getMaxFood();
            GameManager.instance.setMaxFood(currentMaxFood + 500);    // increase the storage size by 500 food

            CheckPurchaseable();
        }
    }
    public void incBeeAmount()
    {
        if (buyBeeAmount <= 1)
            myPurchaseBtns[0].interactable = true;
        buyBeeAmount += 1;
        buyBeeText.text = "Purchase: " + buyBeeAmount;
    }
    public void decBeeAmount()
    {
        buyBeeAmount -= 1;
        buyBeeText.text = "Purchase: " + buyBeeAmount;

    }
}

