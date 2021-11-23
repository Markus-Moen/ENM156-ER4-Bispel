using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class chanceCard : MonoBehaviour
{
    // Start is called before the first frame update

    //public SpriteRenderer chancecard;

    public GameObject[] cards;

    void Start()
    {
      
        
    }

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(0, 10); //currently only randomizes between cards that we have created scenes for

        
        //for debugging purposes
        //Debug.Log("card " + card);

        //loads new scene with chosen card
         CardManager(card);
        //cardTest2.SetActive(true);
    }

    //Handles each individual card
    public void CardManager(int card)
    {
        cards[card].SetActive(true);
        switch (card)
        {
            case 0:
                GameManager.instance.changeBeePercent(1.1f); // 10% More Bees
                break;
            case 1:
                GameManager.instance.changeBeePercent(1.5f); // 50% More bees
                break;
            case 3:
                // Increase honey production by 10%
                break;
            case 4:
               // +1 Flower
                break;
            case 5:
               //  20 % honeyproduction increase    
                break;
            case 6:
                GameManager.instance.changeHoneyPercent(1.25f); // 25% More Honey
                break;
            case 7:
                GameManager.instance.setTermites(true);     // Termites now set to true
                break;
            case 8:
                GameManager.instance.changeBeePercent(0.75f);   // 25% of bees die
                break;
            case 9:
                // For one minute honeyproduction reduced by 90%
                break;
            case 10:
                // +2 Flowers
                break;
        }
    }

    //returns from card scene to store, to be used with button on card scene
    public void ReturnToStore(int card)
    {
        cards[card].SetActive(false);
    }

}
