using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CardManager: MonoBehaviour
{
     public static CardManager instance;

    public GameObject[] chanceCards;
    public GameObject[] questionCards;
    public GameObject wrongCanvas;

    //for card 8
    public float timer = 0;
    public float timerStop = 10;
    public bool timerActive = false;
    public float hiveTmp;

    private void Awake() => instance = this;



    void Start()
    {
       

    }

    /*
    private void Update()
    {
       
    }
    */

    public void RandomizeChanceCard()
    {
        //randomizes a number to pick chancecard
        int card = Random.Range(0, chanceCards.Length); //Add last card when it works

        
        //for debugging purposes
        //Debug.Log("card " + card);

        //loads new scene with chosen card
        ChanceCardManager(card);
        //cardTest2.SetActive(true);
    }

    public void RandomizeQuestionCard()
    {
        int card = Random.Range(0, questionCards.Length);
        QuestionCardManager(card);
    }

    //Handles each individual card
    public void ChanceCardManager(int card)
    {
        chanceCards[card].SetActive(true);
        switch (card)
        {
            case 0:
                GameManager.instance.changeBeePercent(1.1f); // 10% More Bees
                break;
            case 1:
                GameManager.instance.changeBeePercent(1.5f); // 50% More bees
                break;
            case 2:
                // Increase honey production by 10%
                //GameObject.Find("Hive").GetComponent<hiveScript>().setBeeProductivity(1.1f);
                hiveScript.instance.setBeeProductivity(1.2f);
                break;
            case 3:
                // +1 flower
                GameManager.instance.incFlowers(1);
                break;
            case 4:
                //GameObject.Find("Hive").GetComponent<hiveScript>().setBeeProductivity(1.2f);
                hiveScript.instance.setBeeProductivity(1.2f);
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
                // For one minute honeyproduction reduced by 90% 
                hiveTmp = hiveScript.instance.getTotalPercentalChange();
                hiveScript.instance.setBeeProductivity(0.1f);

                CardManager.instance.setTimerActive(true); //starts countdown process in update function in gameManager

                break;
            case 9:
                // +2 Flowers
                GameManager.instance.incFlowers(2);
                break; 

                //infocards
            case 10:
                LogManager.instance.saveToLog(card);
                break;
            case 11:
                LogManager.instance.saveToLog(card);
                break;
            case 12:
                LogManager.instance.saveToLog(card);
                break;
            case 13:
                LogManager.instance.saveToLog(card);
                break;
            case 14:
                LogManager.instance.saveToLog(card);
                break;
            case 15:
                LogManager.instance.saveToLog(card);
                break;
        }
    }

    public void QuestionCardManager(int card)
    {
        questionCards[card].SetActive(true);
    }

     public void ButtonTrue(int card){
            switch (card){
                case 1:
                    wrongCanvas.SetActive(true);
                    chanceCards[11].SetActive(true);
                    // Wrong answer
                break;
                case 6:
                     // Add BeeCoin
                     // Correct answer   
                     break;
                case 7:
                     chanceCards[14].SetActive(true);
                     wrongCanvas.SetActive(true);
                    // Wrong answer
                    break;
                default:
                Debug.Log("QuestionCard does not exist");
                     break;
            }
        questionCards[card].SetActive(false);
    }
    public void ButtonFalse(int card) {
        switch (card) {

            case 1:
                // Add BeeCoin
                // True answer
                break;
            case 6:
                chanceCards[13].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong answer   
                break;
            case 7:
                // Add BeeCoin
                // Correct answer   
                break;
            default:
                Debug.Log("QuestionCard does not exist");
                break;
        }
        questionCards[card].SetActive(false);
    }


    public void ButtonOne(int card){
        switch (card){
            case 0:
                chanceCards[10].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 2:
                // Add BeeCoin
                // Correct Answer
                break;
            case 3:
                wrongCanvas.SetActive(true);
                chanceCards[15].SetActive(true);
                // Wrong Answer
                break;
            case 4:
                chanceCards[14].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 5:
                // Add BeeCoin
                // Correct Answer
                break;
            default:
                Debug.Log("QuestionCard does not exist");
                break;
                    }
        questionCards[card].SetActive(false);
    }

    public void ButtonX(int card){
      switch (card){
            case 0:
                chanceCards[10].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 2:
                chanceCards[11].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 3:
                chanceCards[15].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 4:
                // Add BeeCoin
                // Correct Answer
                break;
            case 5:
                chanceCards[12].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            default:
                Debug.Log("QuestionCard does not exist");
                break;
                    }
        questionCards[card].SetActive(false);
    
    }

    public void ButtonTwo(int card){
      switch (card){
            case 0:
                // Add BeeCoin
                // Correct Answer
                break;
            case 2:
                chanceCards[11].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 3:
                // Add BeeCoin
                // Correct Answer
                break;
            case 4:
                chanceCards[14].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 5:
                chanceCards[15].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            default:
                Debug.Log("QuestionCard does not exist");
                break;
                    }
        questionCards[card].SetActive(false);
    }
    //Hides card again and now Store object is on top. 
    public void ReturnToStoreChance(int card)
    {
        chanceCards[card].SetActive(false);
        wrongCanvas.SetActive(false);
    }

    public void ReturnToStoreQuestion(int card)
    {
        questionCards[card].SetActive(false);

    }

    
    public void setTimer(float n)
    {
        timer = n;

    }

    public void setTimerStop(float n)
    {
        timerStop = n;

    }

    public void setTimerActive(bool n)
    {
        timerActive = n;

    }

    public void setHiveTmp(float n)
    {
        timer = n;

    }

    public float getTimer()
    {
        return timer;

    }

    public float getTimerStop()
    {
        return timerStop;

    }

    public bool getTimerActive()
    {
        return timerActive;

    }

    public float getHiveTmp()
    {
        return timer;

    }
    
}
