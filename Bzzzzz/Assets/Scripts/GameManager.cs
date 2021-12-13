using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Constants - Start values
    private const int startFood = 50;
    private const int startBees = 100;
    private const int startHives = 1;
    private const int startHoney = 100;
    private const int startFlowers = 0;
    private const int startFlowHives = 0;
    private const int startBeeCoins = 0;

    // max values
    private const int max = 1000;
    private const int maxBeesPerHive = 200;
    private int maxHives = max;
    private int maxBees = maxBeesPerHive*startHives;
    private int maxHoney = max;
    private int maxFood = 500;
    private int maxFlowers = 20;
    private int maxFlowHives = max;
    private int maxBeeCoins = 1000;

    // Variables
    private int numOfHoney = startHoney;
    private int numOfFood = startFood;
    private int numOfBees = startBees;
    private int numOfHives = startHives;
    private int numOfBeeCoins = startBeeCoins;

    private int numOfFlowers = startFlowers;
    private int numOfFlowHives = startFlowHives;

    private float beeProductivity = 1f;         // hov much honey one bee produce per time unit
    private float beeKillingRate = 0.95f;       // how many bees die per time unit when starving / parasites
    private float prodPerFlower = 1.03f; // how much productivity is increased per flower

    // Text fields
    Text foodText;
    Text honeyText;
    Text hivesText;
    Text beesText;
    Text beeCoinText;
    Text timerText;
    Text yearlyCostText;
    Text flowersText;
    Text flowHiveText;

    private GameObject outOfFoodText;


    // Timers and related
    private float timer = 0;
    private float delay = 10;   // the time (seconds) before the timer event and timer resets 

    private float seasonTimer = 0;      // timer for the season
    private float seasonDelay = 300;     // length of a season (/year) in seconds 5 min
    private float firstSeasonDelay = 120; //first year is 2 min for demo purpuses
    
    
    public GameObject newYearWarning;
    private bool seasonsIsOn = true;        // true when timer counting down until next year is active




    // Don't think this is used anymore
    private bool deathByStarvation = false;      // true if bees start to die because no food
    // Don't think this is used anymore
    private float percentOfBeesFood = 0.20f;         // 1+percentOfBeesFood = amount of food eaten per bee
    
    


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
    // probably not needed though, since no change of scenes
    /*private void Awake(){
        if(_instance == null ){
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }else if(_instance != this && _instance != null){
            Destroy(this.gameObject);
        }
        
    }
    */
    private void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        seasonTimer = firstSeasonDelay;  // the timer ticks down, not up, so it starts on highest value

        // Initialize text fields
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
        honeyText = GameObject.Find("Honey Text").GetComponent<Text>();
        hivesText = GameObject.Find("Hives Text").GetComponent<Text>();
        flowHiveText = GameObject.Find("Flowhive Text").GetComponent<Text>();
        beesText = GameObject.Find("Bees Text").GetComponent<Text>();
        timerText = GameObject.Find("Timer Text").GetComponent<Text>();
        yearlyCostText = GameObject.Find("Yearly Cost Text").GetComponent<Text>();
        flowersText = GameObject.Find("Flowers Text").GetComponent<Text>();
        beeCoinText = GameObject.Find("Bee Coin Text").GetComponent<Text>();
        outOfFoodText = GameObject.Find("Food Warning");
        newYearWarning = GameObject.Find("newYearWarning");

        // warning texts are not active at start of game
        outOfFoodText.SetActive(false);
        newYearWarning.SetActive(false);

        int hives = numOfHives + numOfFlowHives;        // What is this doing???

        // Set text field texts
        updateFoodTxt();
        updateHoneyTxt();
        updateHivesTxt();
        updateBeesTxt();
        updateTimerTxt();
        updateYearlyCostTxt();
        updateFlowersTxt();
        updateBeeCoinTxt();
        updateFlowHiveTxt();
    }



    // Update is called once per frame
    void Update()
    {
        starvationDeath();                                      // start killing bees if there is no food
        if(seasonsIsOn){ seasonCountDown(); }                   // comment out to disable seasons

        //for chanceCard 9, card 8 in switch statment in cardManager
        chanceCardNineTimer();
        
        
    }


    //-----------------------------------------------------------------------------------------------//


    // updates all text fields
    public void reload(){
        updateFoodTxt();
        updateHoneyTxt();
        updateHivesTxt();
        updateFlowHiveTxt();
        updateBeesTxt();
        updateBeeCoinTxt();
        updateTimerTxt();
        updateYearlyCostTxt();
        updateFlowersTxt();
    }


    // updating one text field

    private void updateFoodTxt(){
        foodText.text =  numOfFood + " / " + maxFood;
    }
    private void updateHoneyTxt(){
        honeyText.text =  numOfHoney + " / " + maxHoney;
    }
    private void updateHivesTxt(){
        int hives = numOfHives;
        hivesText.text = hives.ToString();
    }
    private void updateFlowHiveTxt(){
        int hives = numOfFlowHives;
        flowHiveText.text = hives.ToString();
    }
    private void updateBeesTxt(){
        beesText.text =  numOfBees + " / " + maxBees;
    }
    private void updateBeeCoinTxt(){
        beeCoinText.text =  numOfBeeCoins + " / " + maxBeeCoins;
    }
    private void updateTimerTxt(){
        timerText.text = "Time till next year: " + getTime();
    }
    private void updateYearlyCostTxt(){
        yearlyCostText.text = "Yearly cost: "  + "\nFood: " + (int) Mathf.Floor(numOfBees*percentOfBeesFood)
                                + "\nHoney: " + yearlyHoneyCost();
    }
    private void updateFlowersTxt(){
        flowersText.text = numOfFlowers + " / " + maxFlowers;
    }


    

    // loads game over scene
    private void gameOver(){
        SceneManager.LoadScene("GameOver");
    }


    // Start killing bees
    private void startKillFood(){                                               // Don't think this is used anymore
        deathByStarvation = true;
    }

    // stop killing bees
    private void stopKillFood(){                                               // Don't think this is used anymore
        deathByStarvation = false;
    }

    // updates timer for (and executes) bee death
    // if deathByStarvation is set to true 
    private void starvationDeath(){                                               // Don't think this is used anymore
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


    // The timer code for chance card #9
    private void chanceCardNineTimer(){
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

            flowerScript.instance.removeFlowers(numOfFlowers);// Kill all flowers at end of year
            openNewYearWarning();
                                                                                                        
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
    //------------------------------------increase variables-----------------------------------------//


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
            //stopKillFood();
            deactivateFoodWarning();
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


    // adds n to the players bee coins
    // returns any leftovers (if the max limit is hit)
    public int incPlayerBeeCoins(int n){
        numOfBeeCoins += n;
        int leftovers = 0;
        // cannot have more than the max amount of bee coins
        if(numOfBeeCoins > maxBeeCoins){
            leftovers = numOfBeeCoins - maxBeeCoins;
            numOfBeeCoins = maxBeeCoins;
        }
        updateBeeCoinTxt();
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
        hiveScript.instance.setFlowerProductivity(prodPerFlower, numOfFlowers);
        updateYearlyCostTxt();
        updateFlowersTxt();
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
            updateFlowHiveTxt();
            updateYearlyCostTxt();
            return true;
        }
        return false;
    }


    //-----------------------------------------------------------------------------------------------//
    //-------------------------------------Decrease variables----------------------------------------//




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
            activateFoodWarning();
            //startKillFood();        // no food => bees start to die
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

    // subtracts n from numOfBeeCoins - if this does not 
    // result in a negative numOfHoney
    // - returns true if subtraction is successful
    // - returns false if it would lead to a negative value of
    //   numOfBeeCoins
    public bool decPlayerBeeCoins(int n){
        if(numOfBeeCoins - n < 0){
            updateBeeCoinTxt();
            return false;
        }
        numOfBeeCoins -= n;
        updateBeeCoinTxt();
        return true;
    }

    // subtracts n from numOfFlowers
    // cannot be lower than 0
    public void decFlowers(int n){
        numOfFlowers -= n;
        if(numOfFlowers <= 0){
            numOfFlowers = 0;
        }
        hiveScript.instance.setFlowerProductivity(prodPerFlower, numOfFlowers);
        updateFlowersTxt();
        updateYearlyCostTxt();
    }




    //-----------------------------changing variables by a percentage---------------------------


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

    public int getPlayerBeeCoins(){
        return numOfBeeCoins;
    }

    public int getPlayerFlowHives(){
        return numOfFlowHives;
    }
    public int getPlayerFlowers()
    {
        return numOfFlowers;
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

    public int getMaxHives(){
        return maxHives;
    }

    public int getMaxFlowHives(){
        return maxFlowHives;
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




    //-------------------------------------------------------------------------------
    //----------------Activation and deactivation of warnings------------------------
    //-------------------------------------------------------------------------------


    //for close button on new year warning
    //Do we want to properly pause game when warning is up?
    public void closeNewYearWarning(){
        Debug.Log("close button");
        newYearWarning = GameObject.Find("newYearWarning");
        newYearWarning.SetActive(false);  //remove warning card
        seasonsIsOn = true;               //start timer until next year again
        //seasonTimer = seasonDelay;        //set so new season/year start when closing
    }

    //activate warning card informing player.
    private void openNewYearWarning(){
        newYearWarning.SetActive(true);
        seasonsIsOn = false;            // Stop countdown until button is pressed
    }


    private void activateFoodWarning(){
        outOfFoodText.SetActive(true);
    }
    private void deactivateFoodWarning(){
        outOfFoodText.SetActive(false);
    }
}
