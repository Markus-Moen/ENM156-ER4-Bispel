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

    public GameObject shop;

    void Start()
    {
      
        
    }

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(1, 3); //currently only randomizes between cards that we have created scenes for
        
        //for debugging purposes
        Debug.Log("card " + card);

        //loads new scene with chosen card
        CardManager(card);
    }

    //Handles each individual card
    public void CardManager(int card)
    {
        switch (card)
        {
            case 1:
                //todo: add concequences eg +honey

                //load scene with chance card 1
                SceneManager.LoadScene("ChanceCard#1");
                break;
            case 2:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#2");
                break;

            case 3:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#3");
                break;

            case 4:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#4");
                break;
            case 5:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#5");
                break;
            case 6:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#6");
                break;
            case 7:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#7");
                break;
            case 8:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#8");
                break;
            case 9:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#9");
                break;
            case 10:
                //todo: add concequences eg +honey

                //load scene with chance card 2
                SceneManager.LoadScene("ChanceCard#10");
                break;
        }
    }

    //returns from card scene to store, to be used with button on card scene
    public void ReturnToStore()
    {
        Debug.Log("in return to store");
        SceneManager.LoadScene("GameView");
        GameManager.instance.reload();
    }

}
