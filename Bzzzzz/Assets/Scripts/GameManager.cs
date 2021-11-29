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
    private const int startFlowHives = 0;

    // max values
    private const int max = 1000;
    private const int maxBeesPerHive = max;
    private int maxHives = max;
    private int maxBees = maxBeesPerHive*startHives;
    private int maxHoney = max;
    private int maxFood = max;
    private int maxFlowers = 100;
    private int maxFlowHives = max;

    // Variables
    private int numOfHoney = startHoney;
    private int numOfFood = startFood;
    private int numOfBees = startBees;
    private int numOfHives = startHives;

    private int numOfFlowers = startFlowers;
    private int numOfFlowHives = startFlowHives;

    private float beeProductivity = 1f;     // hov much honey one bee produce per time unit
    private float beeKillingRate = 0.95f;   // how many bees die per time unit when starving / parasites

    // Text fields
    Text foodText;
    Text honeyText;
    Text hivesText;
    Text beesText;
    Text timerText;
    Text yearlyCostText;


    private float timer = 0;
    private float delay = 10;   // the time (seconds) before the timer event and timer resets 

    private float seasonTimer = 0;      // timer for the season
    private float seasonDelay = 65;     // length of a season (/year) in seconds
    private float percentOfBeesFood = 0.05f;         // 1+percentOfBeesFood = amount of food eaten per bee




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
        seasonTimer = seasonDelay;  // the timer ticks down, not up, so it starts on highest value

        // Initialize text fields
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
        honeyText = GameObject.Find("Honey Text").GetComponent<Text>();
        hivesText = GameObject.Find("Hives Text").GetComponent<Text>();
        beesText = GameObject.Find("Bees Text").GetComponent<Text>();
        timerText = GameObject.Find("Timer Text").GetComponent<Text>();
        yearlyCostText = GameObject.Find("Yearly Cost Text").GetComponent<Text>();

        // Set text field texts
        int hives = numOfHives + numOfFlowHives;
        //foodText.text = "Food: " + numOfFood + " / " + maxFood;
        //honeyText.text = "Honey: " + numOfHoney + " / " + maxHoney;
        //hivesText.text = "Hives: " + hives + " (Flow: " + numOfFlowHives + ")";
        //beesText.text = "Bees: " + numOfBees + " / " + maxBees;
        //timerText.text = "Time till next season: " + getTime();
        //yearlyCostText.text = "Yearly cost: "  + "\nFood: " + (int) Mathf.Floor(numOfBees*percentOfBeesFood)
        //                        + "\nHoney: " + yearlyHoneyCost();
        updateFoodTxt();
        updateHoneyTxt();
        updateHivesTxt();
        updateBeesTxt();
        updateTimerTxt();
        updateYearlyCostTxt();
        
    }



    // Update is called once per frame
    void Update()
    {
        // start killing bees if there is no food
        starvationDeath();

        // comment out to disable seasons
        //seasonCountDown();

        //for chanceCard 9, card 8 in switch statment in cardManager
        if (CardManager.instance.getTimerActive())
        {
            Debug.Log("Timer active");
            if (CardManager.instance.getTimer() < CardManager.instance.getTimerStop()) //until stoptime is reached, keep counting up
            {
                CardManager.instance.setTimer(CardManager.instance.getTimer() + Time.deltaTime);
            }
            else
            {
                CardManager.instance.setTimer(0);
                CardManager.instance.setTimerActive(false);
                //GameObject.Find("Hive").GetComponent<hiveScript>().setBeeProductivity(hiveTmp);
                hiveScript.instance.setBeeProductivity(CardManager.instance.getHiveTmp());
                //GameObject.Find("Hive").GetComponent<hiveScript>().beeProductivity = hiveTmp; //restore hive productivity
                Debug.Log("Timer done");
            }

        }
    }


    //-----------------------------------------------------------------------------------------------//


    // updates all text fields
    public void reload(){
        int hives = numOfHives + numOfFlowHives;
        foodText.text = "Food: " + numOfFood + " / " + maxFood;
        honeyText.text = "Honey: " + numOfHoney + " / " + maxHoney;
        hivesText.text = "Hives: " + hives + " (Flow: " + numOfFlowHives + ")";
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
        int hives = numOfHives + numOfFlowHives;
        hivesText.text = "Hives: " + hives + " (Flow: " + numOfFlowHives + ")";
    }
    private void updateBeesTxt(){
        beesText.text = "Bees: " + numOfBees + " / " + maxBees;
    }
    private void updateTimerTxt(){
        timerText.text = "Time till next season: " + getTime();
    }
    private void updateYearlyCostTxt(){
        yearlyCostText.text = "Yearly cost: "  + "\nFood: " + (int) Mathf.Floor(numOfBees*percentOfBeesFood)
                                + "\nHoney: " + yearlyHoneyCost();
    }

    // loads game over scene
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

    // updates timer for (and executes) bee death
    // if deathByStarvation is set to true 
    private void starvationDeath(){
        if(deathByStarvation){
            timer += Time.deltaTime;        // update timer
            if(timer >= delay){
                // kill a number of bees
                changeBeePercent(beeKillingRate);
                // reset timer
                timer = 0;
            }
        }
    }


    // counts down until next year
    // when timer reaches zero, the yearly cost and the amount of bees that die because of starvation is subtracted
    private void seasonCountDown(){
        if(seasonTimer > 0){
            seasonTimer -= Time.deltaTime;  // update timer (starts at highest possible time)
        }else{
            seasonTimer = seasonDelay;      // reset timer

            // if there is not enough food to keep all of the bees alive
            // the ones who don't get food die of starvation
            int foodEatenByBees = Mathf.FloorToInt(numOfBees*percentOfBeesFood);
            int missingFood = numOfFood - foodEatenByBees;
            
            if(missingFood < 0){
                missingFood = Mathf.Abs(missingFood);                              // make positive
                decPlayerBees(Mathf.RoundToInt(missingFood / percentOfBeesFood));   // kill # bees corresponding to missing food
            }
            decPlayerFood(foodEatenByBees);   // subtract starving bees (caused by too little food over winter)
            
            // set numOfHoney = 0 if there is not enough honey to pay for the yearly cost
            bool b = decPlayerHoney(yearlyHoneyCost());          // subtract yearly cost
            if(!b){ 
                numOfHoney = 0;
                updateHoneyTxt();
            }
                                                                                                        
        }
        updateTimerTxt();   // update timer text field
        
    }

    // The yearly cost from beeing (get it) a beekeeper
    private int yearlyHoneyCost(){  // just a placeholder formula
                // costs to have hives
                // costs even more to have automated hives
                // costs to water and tend flowers
                // costs to have storage and land
        return (numOfHives + numOfFlowHives*100 + numOfFlowers*2 + maxHoney/8 + maxFood/8 + maxFlowers/2); // also factor in pestilence thingy
    }

    // returns string of seasonTimer set in minutes and seconds with a colon in between
    private string getTime(){
        int minutes = Mathf.FloorToInt(seasonTimer) / 60;
        int seconds = Mathf.FloorToInt(seasonTimer) % 60;
        string minutesString = (minutes < 10) ? "0" + minutes : "" + minutes;   // adds a zero in front of number if < 10
        string secondsString = (seconds < 10) ? "0" + seconds : "" + seconds;   // adds a zero in front of number if < 10
        

        return minutesString + ":" + secondsString;
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
        updateYearlyCostTxt();
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
        updateYearlyCostTxt();
        return leftovers;
    }

    // When upgrading a hive to a flow hive, it is 
    //no longer considered a normal hive
    // Returns true if upgrade was succesful
    // false otherwise
    public bool upgradeHive(){
        if(numOfHives > 0){
            numOfHives -= 1;
            numOfFlowHives += 1;
            updateHivesTxt();
            updateYearlyCostTxt();
            return true;
        }
        return false;
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

    // subtracts n from numOfHives
    // cannot be lower than 0
    public void decPlayerHives(int n){         // Should probably call some function to change game state if all hives are gone
        numOfHives -= n;                       // and/or if hives start dissapearing?
        if(numOfHives <= 0){
            numOfHives = 0;                    // don't know if the player can loose hives, and if they can reach 0
        }
        updateHivesTxt();
        updateYearlyCostTxt();
    } 

    // subtracts n from numOfFlowers
    // cannot be lower than 0
    public void decFlowers(int n){
        numOfFlowers -= n;
        if(numOfFlowers <= 0){
            numOfFlowers = 0;
        }
        updateYearlyCostTxt();
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

    public int getPlayerFlowHives(){
        return numOfFlowHives;
    }

    public float getPercentOfBeesFood(){
        return percentOfBeesFood;
    }


    public int getMaxFood(){
        return maxFood;
    }

    public int getMaxHoney(){
        return maxHoney;
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
        updateYearlyCostTxt();
    }

    public void setMaxHoney(int n){
        maxHoney = n;
        updateHoneyTxt();
        updateYearlyCostTxt();
    }

    public void setDelay(float f){
        delay = f;
    }

    public void setMaxFlowers(int n)
    {
        maxFlowers = n;
        updateYearlyCostTxt();
    }

    

    
}
