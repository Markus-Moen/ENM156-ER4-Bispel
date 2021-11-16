using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// It is what it sounds like
// Contains constants, variables and functions for the "player"
// such as a function for incrementing the honey in the player storage

public class GameManager : MonoBehaviour
{

    // Constants - Start values
    public const int startFood = 100;
    public const int startBees = 100;
    public const int startHives = 1;
    public const int startHoney = 0;

    // Constants - max values
    public const int max = 10000;
    public const int maxHoney = max;
    public const int maxFood = max;
    public const int maxBees = max;
    public const int maxHives = max;

    // Variables
    public int numOfHoney;
    public int numOfFood;
    public int numOfBees;
    public int numOfHives;

    // The different states of the game - might be useful
    public GameState state;


    public static GameManager Instance;

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set variables to start values
        numOfFood = startFood;
        numOfBees = startBees;
        numOfHoney = startHoney;
        numOfHives = startHives;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds collectedHoney to the players stored honey
    // and returns any leftovers (if the max limit is hit)
    public int incPlayerHoney(int collectedHoney){
        numOfHoney += collectedHoney;
        int leftovers = 0;

        if(numOfHoney > maxHoney){
            leftovers = numOfHoney - maxHoney;
        }

        return leftovers;
    }


    public void updateGameState(){
        //TODO
    }
}


// different states of the game
// - running - game is running
// - lost    - you lost, all bees are dead/food is gone
// might be useful or utter shit.
public enum GameState{
    Running,
    Lost
}
