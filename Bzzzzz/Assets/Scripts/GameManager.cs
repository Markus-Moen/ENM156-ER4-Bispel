using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Constants - Start values
    private const int startFood = 100;
    private const int startBees = 100;
    private const int startHives = 1;
    private const int startHoney = 100;
    private const int startFlowers = 0;

    // max values
    private const int max = 10000;
    private const int maxBeesPerHive = max;
    private int maxHives = max;
    private int maxBees = maxBeesPerHive*startHives;
    private int maxHoney = max;
    private int maxFood = max;
    private int maxFlowers = 100;

    // Variables
    private int numOfHoney = startHoney;
    private int numOfFood = startFood;
    private int numOfBees = startBees;
    private int numOfHives = startHives;
    private int numOfFlowers = startFlowers;

    private float beeProductivity = 1f;     // hov much honey one bee produce per time unit
    private float beeKillingRate = 0.95f;   // how many bees die per time unit when starving / parasites

    // Text fields
    Text foodText;
    Text honeyText;
    Text hivesText;
    Text beesText;


    private float timer = 0;
    private float delay = 10;   // the time (seconds) before the timer event and timer resets 


    // should probably be in hiveScript
    private bool deathByStarvation = false;      // true if bees start to die because no food
    
    


    // GameManager singleton
    private static GameManager _instance;
    public static GameManager instance { get { 
        if(_instance == null){
            GameObject go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }
        return _instance; }             
    }


    // make sure there are no other instances of GameManager
    private void Awake(){
        if(_instance == null ){
            DontDestroyOnLoad(this.gameObject);                         // I'm afraid this part might create some problems
            _instance = this;                                            // a bit tricky to test at the moment...
        }else if(_instance != this && _instance != null){
            Destroy(this.gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize text fields
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
        honeyText = GameObject.Find("Honey Text").GetComponent<Text>();
        hivesText = GameObject.Find("Hives Text").GetComponent<Text>();
        beesText = GameObject.Find("Bees Text").GetComponent<Text>();

        // Set text field texts
        foodText.text = "Food: " + numOfFood + " / " + maxFood;
        honeyText.text = "Honey: " + numOfHoney + " / " + maxHoney;
        hivesText.text = "Hives: " + numOfHives;
        beesText.text = "Bees: " + numOfBees + " / " + maxBees;
        
    }

    // Update is called once per frame
    void Update()
    {
        // start killing bees if there is no food
        if(deathByStarvation){
            timer += Time.deltaTime;
            if(timer >= delay){
                // kill a number of bees
                changeBeePercent(beeKillingRate);
                // reset timer
                timer = 0;
            }
        }
             
    }


    //-----------------------------------------------------------------------------------------------//


    // updates all text fields
    public void reload(){
        foodText.text = "Food: " + numOfFood + " / " + maxFood;
        honeyText.text = "Honey: " + numOfHoney + " / " + maxHoney;
        hivesText.text = "Hives: " + numOfHives;
        beesText.text = "Bees: " + numOfBees + " / " + maxBees;
    }


    // updating one text field

    private void updateFoodTxt(){
        foodText.text = "Food: " + numOfFood + " / " + maxFood;
    }
    private void updateHoneyTxt(){
        honeyText.text = "Honey: " + numOfHoney + " / " + maxHoney;
    }
    private void updateHivesTxt(){
        hivesText.text = "Hives: " + numOfHives;
    }
    private void updateBeesTxt(){
        beesText.text = "Bees: " + numOfBees + " / " + maxBees;
    }

    private void gameOver(){
        SceneManager.LoadScene("GameOver");
    }


    // Start killing bees
    private void startKillFood(){
        deathByStarvation = true;
    }

    // stop killing bees
    private void stopKillFood(){
        deathByStarvation = false;
    }


    //-----------------------------------------------------------------------------------------------//


    // Adds n to the players stored honey
    // and returns any leftovers (if the max limit is hit)
    public int incPlayerHoney(int n){
        numOfHoney += n;
        int leftovers = 0;
        // cannot have more than the max amount of honey
        if(numOfHoney > maxHoney){
            leftovers = numOfHoney - maxHoney;
            numOfHoney = maxHoney;
        }
        updateHoneyTxt();
        return leftovers;
    }

    // adds n to the players bees
    // returns any leftovers (if the max limit is hit)
    public int incPlayerBees(int n){
        numOfBees += n;
        int leftovers = 0;
        // cannot have more than the max amount of bees
        if(numOfBees > maxBees){
            leftovers = numOfBees - maxBees;
            numOfBees = maxBees;
        }
        updateBeesTxt();
        return leftovers;
    }

    // adds n to the players bee food
    // returns any leftovers (if the max limit is hit)
    public int incPlayerFood(int n){
        numOfFood += n;
        int leftovers = 0;
        // cannot have more than the max amount of food
        if(numOfFood > maxFood){
            leftovers = numOfFood - maxFood;
            numOfFood = maxFood;
        }
        // No bees die if there is food
        if(numOfFood > 0){             // only stop the killing if there is food an there are no termites
            stopKillFood();
        }
        updateFoodTxt();
        return leftovers;
    }

    // adds n to the players hives
    // returns any leftovers (if the max limit is hit)
    public int incPlayerHives(int n){
        numOfHives += n;
        int leftovers = 0;
        // cannot have more than the max amount of hives
        if(numOfHives > maxHives){
            leftovers = numOfHives - maxHives;
            numOfHives = maxHives;
        }
        maxBees = maxBeesPerHive * numOfHives;  // the max number of bees increases with every added hive
        updateHivesTxt();
        updateBeesTxt();
        return leftovers;
    }

    // adds n flowers
    // returns any leftovers (if the max limit is hit)
    public int incFlowers(int n)
    {   numOfFlowers += n;
        int leftovers = 0;
        if(numOfFlowers > maxFlowers)
        {
            leftovers = numOfFlowers - maxFlowers;
            numOfFlowers = maxFlowers;
        }
        return leftovers;
    }


    //-----------------------------------------------------------------------------------------------//




    // subtracts n from numOfHoney - if this does not 
    // result in a negative numOfHoney
    // - returns true if subtraction is successful
    // - returns false if it would lead to a negative value of
    //   numOfHoney
    public bool decPlayerHoney(int n){
        if(numOfHoney - n < 0){
            updateHoneyTxt();
            return false;
        }
        numOfHoney -= n;
        updateHoneyTxt();
        return true;
    }

    // subtracts n from numOfBees
    // does something if all bees die
    public void decPlayerBees(int n){
        numOfBees -= n;
        if(numOfBees <= 0){
            numOfBees = 0;
            //TODO
            //gameOver();
        }
        updateBeesTxt();
    }


    // subtracts n from numOfFood
    // - returns false if numOfFood <= 0 (and sets it to 0 at the lowest)
    // - returns true otherwise
    public bool decPlayerFood(int n){         // Should probably call some function to change game state so that bees start dying
        numOfFood -= n;
        if(numOfFood <= 0){
            numOfFood = 0;
            updateFoodTxt();
            startKillFood();        // no food => bees start to die
            return false;
        }
        updateFoodTxt();
        return true;
    }

    // subtracts n from numOfFood
    // - returns false if numOfHives <= 0 (and sets it to 0 at the lowest)
    // - returns true otherwise
    public bool decPlayerHives(int n){         // Should probably call some function to change game state if all hives are gone
        numOfHives -= n;                       // and/or if hives start dissapearing?
        if(numOfHives <= 0){
            numOfHives = 0;                    // don't know if the player can loose hives, and if they can reach 0
            updateHivesTxt();
            return false;
        }
        updateHivesTxt();
        return true;
    } 

    // multiplies numOfHoney by the argument f
    // effectively changing the amount of honey by a percentage
    public void changeHoneyPercent(float f){
        numOfHoney = (int) Mathf.Floor(numOfHoney*f);  
        updateHoneyTxt();      
    }

    // multiplies numOfHoney by the argument f
    // effectively changing the amount of honey by a percentage
    public void changeBeePercent(float f){
        numOfBees = (int) Mathf.Floor(numOfBees*f);  
        updateBeesTxt();
        // TODO if bees = 0
    }


    

    //-----------------------------------------------------------------------------------------------//



    // Getters
    public int getPlayerHoney(){
        return numOfHoney;
    }

    public int getPlayerBees(){
        return numOfBees;
    }

    public int getPlayerHives(){
        return numOfHives;
    }

    public int getPlayerFood(){
        return numOfFood;
    }


    public int getMaxFood(){
        return maxFood;
    }
    
    public int getMaxBees(){
        return maxBees;
    }

    public int getMaxFlowers()
    {
        return maxFlowers;
    }



    // Setters

    public void setMaxFood(int n){
        maxFood = n;
        updateFoodTxt();
    }

    public void setMaxHoney(int n){
        maxHoney = n;
        updateHoneyTxt();
    }

    public void setDelay(float f){
        delay = f;
    }

    public void setMaxFlowers(int n)
    {
        maxFlowers = n;
    }

    

    
}
