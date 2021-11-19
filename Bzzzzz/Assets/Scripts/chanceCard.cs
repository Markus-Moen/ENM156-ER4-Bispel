using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class chanceCard : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer chancecard;

    void Start()
    {
        
    }

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(0, 9);
        Debug.Log("card " + card);

        //test code, doesn't work don't use
        //GameObject card2 = GameObject.FindGameObjectWithTag("chanceCard#1");
        //card2.GetComponent<Renderer>().enabled = true;
        //card2.SetActive(true);
        //chancecard.GetComponent<Renderer>().enabled = true;
        //chancecard.enabled = true;
        chancecard = GetComponent<SpriteRenderer>();
        chancecard.enabled = true;
    }

    //enables consequences for each card
    public void CardManager(int card)
    {
        switch (card)
        {
            case 0:
                GameObject card1 = GameObject.FindGameObjectWithTag("Chancecard#1");
                card1.GetComponent<Renderer>().enabled = true;
                break;
        }
    }

}
