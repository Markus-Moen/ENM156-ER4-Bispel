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
    public GameObject correctCanvas;

    //for card 8
    public float timer = 0;
    public float timerStop = 30;
    public bool timerActive = false;
    public float hiveTmp;

    private void Awake() => instance = this;

    private int beeCoins = 50;
    private int iteratorQ = 0;
    private int iteratorC = 0;

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
        //int card = Random.Range(0, chanceCards.Length); //Add last card when it works
        int[] arrC = {14,3,6,5,7,8,1,2,0,11,13,12,15,4,9,10};
        int card = arrC[iteratorC];
        iteratorC = (iteratorC + 1) % 15;
        //for debugging purposes
        //Debug.Log("card " + card);

        //loads new scene with chosen card
        ChanceCardManager(card);
        //cardTest2.SetActive(true);
    }

    public void RandomizeQuestionCard()
    {
        //int card = Random.Range(0,questionCards.Length); 
        int[] arrQ = { 0, 2, 5, 6, 3, 1, 7, 4 };
        int card = arrQ[iteratorQ];
        iteratorQ = (iteratorQ + 1) % 7;

        questionCards[card].SetActive(true);
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
                GameManager.instance.changeBeePercent(1.3f); // 30% More bees
                break;
            case 2:
                // Increase honey production by 10%
                hiveScript.instance.setBeeProductivity(1.1f);
                break;
            case 3:
                // +1 flower
                flowerScript.instance.addFlowers(1);
                break;
            case 4:
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
                // For 1 minute (30 seconds for demo) honeyproduction reduced by 90% 
                hiveTmp = hiveScript.instance.getTotalPercentalChange();
                hiveScript.instance.setBeeProductivity(0.1f);

                CardManager.instance.setTimerActive(true); //starts countdown process in update function in gameManager

                break;
            case 9:
                // +2 Flowers
                flowerScript.instance.addFlowers(2);
                break; 

                //infocards
            case 10:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
            case 11:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
            case 12:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
            case 13:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
            case 14:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
            case 15:
                LogManager.instance.saveToLog(chanceCards[card]);
                break;
        }
    }

     public void ButtonTrue(int card){
            switch (card){
                case 1:
                    wrongCanvas.SetActive(true);
                    chanceCards[11].SetActive(true);
                    // Wrong answer
                break;
                case 6:
                correctCanvas.SetActive(true);
                GameManager.instance.incPlayerBeeCoins(beeCoins);
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
                correctCanvas.SetActive(true);
                break;
            case 6:
                chanceCards[13].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong answer   
                break;
            case 7:
                // Add BeeCoin
                // Correct answer   
                correctCanvas.SetActive(true);
                GameManager.instance.incPlayerBeeCoins(beeCoins);
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
                correctCanvas.SetActive(true);
                GameManager.instance.incPlayerBeeCoins(beeCoins);
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
                correctCanvas.SetActive(true);
                GameManager.instance.incPlayerBeeCoins(beeCoins);
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
                correctCanvas.SetActive(true);
                GameManager.instance.incPlayerBeeCoins(beeCoins);
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
                // Correct Answe
                GameManager.instance.incPlayerBeeCoins(beeCoins);
                correctCanvas.SetActive(true);
                Debug.Log("Card 1 Correct answer");
                break;
            case 2:
                chanceCards[11].SetActive(true);
                wrongCanvas.SetActive(true);
                // Wrong Answer
                break;
            case 3:
                // Add BeeCoin
                // Correct Answer
                GameManager.instance.incPlayerBeeCoins(beeCoins);
                correctCanvas.SetActive(true);
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

    public void ReturnToStoreCorrect()
    {
        correctCanvas.SetActive(false);
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
