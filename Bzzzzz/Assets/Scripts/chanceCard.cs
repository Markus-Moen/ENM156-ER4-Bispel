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

    //for card 8
    private float timer = 0;
    private float timerStop = 10;
    private bool timerActive = false;
    private float hiveTmp;
    

    void Start()
    {
      
        
    }

    private void Update()
    {
        //for card 9 (8 in switch statment)
        if (timerActive)
        {
            //Debug.Log("Timer active");
            if (timer < timerStop) //until stoptime is reached, keep counting up
            {
                timer += Time.deltaTime;
            }
            else 
            {
                timer = 0;
                timerActive = false;
                GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation = hiveTmp; //restore hive productivity
                //Debug.Log("Timer done");
            }
            
        }
    }

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(0, 10); //Add last card when it works

        
        //for debugging purposes
        Debug.Log("card " + card);

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
            case 2:
                GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation = 1.1f;
                Debug.Log(GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation);
                // Increase honey production by 10%
                break;
            case 3:
               // +1 Flower
                break;
            case 4:
                GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation = 1.2f;
                Debug.Log(GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation);
                //  20 % honeyproduction increase    
                break;
            case 5:
                GameManager.instance.changeHoneyPercent(1.25f); // 25% More Honey
                break;
            case 6:
                Parasites.instance.startParasites();     // parasites now set to true
                break;
            case 7:
                GameManager.instance.changeBeePercent(0.75f);   // 25% of bees die
                break;
            case 8:
                // For one minute honeyproduction reduced by 90% Waiting NOT WORKING!
                hiveTmp = GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation;
                GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation = 0.1f;
                //new WaitForSeconds(6);

                timerActive = true; //starts process in update

                //GameObject.Find("Hive").GetComponent<hiveScript>().productivityApproximation = tmp;
                break;
            case 9:
                // +2 Flowers
                break; 
        }
    }

    //Hides card again and now Store object is on top. 
    public void ReturnToStore(int card)
    {
        cards[card].SetActive(false);
    }

}
