using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
	public GameObject shop;
    public int ownedHoney;		        // The players owned honey. This is just a placeholder value.
    public TMP_Text honeyText;          // Dispayed honey in-game.
    public ShopItemSO[] shopItemsSO;    // Array of all scriptable objects.
    public ShopTemplate[] shopPanels;   // Array of the used shop panels.
    public GameObject[] shopPanelsGO;   // Array used to handle which of the items in the SO-array that should be active.
    public Button[] myPurchaseBtns;     // Array of all purchase buttons.

    // Start is called before the first frame update
    void Start()
    {
    	ownedHoney = GameManager.instance.getPlayerHoney();
        // Only activates the shop panels that are being used  
        // since there can be more scriptable objects than panels.
        for (int i = 0; i < shopItemsSO.Length; i++)
            shopPanelsGO[i].SetActive(true);        
        honeyText.text = "Honey: " + ownedHoney.ToString(); 
        LoadItems();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
        //Currently not used.  
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
    }

    //Updates whether wach button should be enabled or not. 
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (ownedHoney >= shopItemsSO[i].baseCost)
                myPurchaseBtns[i].interactable = true;
            else
                myPurchaseBtns[i].interactable = false;
        }
    }

    // Checks if the player's current honey is enough to buy an item. 
    // It it is, the player's owned honey is updated aswell as the 
    // number of owned quantity of the purchased item. 
    public void buyItem(int btn)
    {
         if (ownedHoney >= shopItemsSO[btn].baseCost)
        {
            ownedHoney = ownedHoney - shopItemsSO[btn].baseCost;
            GameManager.instance.decPlayerHoney(shopItemsSO[btn].baseCost);

            honeyText.text = "Honey: " + ownedHoney.ToString();
            shopItemsSO[btn].owned += 1;
            shopPanels[btn].quantityTxt.text = "Owned: " + shopItemsSO[btn].owned.ToString();
            CheckPurchaseable();
        }
    }
    public void closeShop(){
    	shop.SetActive(false);
    }
}
