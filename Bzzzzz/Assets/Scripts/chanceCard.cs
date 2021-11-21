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

    void Start()
    {
      
        
    }

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(0, 2); //currently only randomizes between cards that we have created scenes for
        
        //for debugging purpuces
        Debug.Log("card " + card);

        //loads new scene with chosen card
        CardManager(card);
    }

    //Handles each individual card
    public void CardManager(int card)
    {
        switch (card)
        {
            case 0:
                //todo: add concequences eg +honey

                //load scene with chance card 1
                SceneManager.LoadScene("ChanceCard#1");
                break;
            case 1:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#2");
                break;
        }
    }

    //returns from card scene to store, to be used with button on card scene
    public void ReturnToStore()
    {
        Debug.Log("in return to store");
        SceneManager.LoadScene("Shop");
    }

}
