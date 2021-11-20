using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Constants - Start values
    private const int startFood = 100;
    private const int startBees = 100;
    private const int startHives = 1;
    private const int startHoney = 100;

    // Constants - max values
    private const int max = 10000;
    private const int maxHoney = max;
    private const int maxFood = max;
    private const int maxBees = max;
    private const int maxHives = max;

    // Variables
    private int numOfHoney = startHoney;
    private int numOfFood = startFood;
    private int numOfBees = startBees;
    private int numOfHives = startHives;

    // Text fields
    Text foodText;
    Text honeyText;
    Text hivesText;
    Text beesText;


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
        foodText.text = "Food: " + numOfFood;
        honeyText.text = "Honey: " + numOfHoney;
        hivesText.text = "Hives: " + numOfHives;
        beesText.text = "Bees: " + numOfBees;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //-----------------------------------------------------------------------------------------------//


    private void updateFoodTxt(){
        foodText.text = "Food: " + numOfFood;
    }
    private void updateHoneyTxt(){
        honeyText.text = "Honey: " + numOfHoney;
    }
    private void updateHivesTxt(){
        hivesText.text = "Hives: " + numOfHives;
    }
    private void updateBeesTxt(){
        beesText.text = "Bees: " + numOfBees;
    }


    //-----------------------------------------------------------------------------------------------//



    // Adds n to the players stored honey
    // and returns any leftovers (if the max limit is hit)
    public int incPlayerHoney(int n){
        numOfHoney += n;
        int leftovers = 0;

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
        if(numOfFood > maxFood){
            leftovers = numOfFood - maxFood;
            numOfFood = maxFood;
        }
        updateFoodTxt();
        return leftovers;
    }

    // adds n to the players hives
    // returns any leftovers (if the max limit is hit)
    public int incPlayerHives(int n){
        numOfHives += n;
        int leftovers = 0;
        if(numOfHives > maxHives){
            leftovers = numOfHives - maxHives;
            numOfHives = maxHives;
        }
        updateHivesTxt();
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
}
