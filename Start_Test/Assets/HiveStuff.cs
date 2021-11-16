using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains constants, variables and functions related to the hives (not the band)
// such as a function for incrementing the honey in the 

public class HiveStuff : MonoBehaviour
{
    // Probably not the best way to do this...
    public static HiveStuff Instance;

    void Awake(){
        Instance = this;
    } 

    // constants and variables
    public const int maxHoneyPerHive = 100;
    public int hiveHoney;
    
    // Time variables
    public float timer;
    public int delayAmount = 3;     // # of seconds before the honey bumps up in the hive

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update time
        timer += Time.deltaTime;
        //increment honey in hive
        inchiveHoney();     
    }






    // increments honey in hive i enough time has passed
    private void inchiveHoney(){
        if(timer >= delayAmount){
            timer = 0;      // reset timer

            hiveHoney += GameManager.Instance.numOfBees/10;     // divide by 10 since we have so many bees...

            // Cannot get more honey than the max number per hive*number of hives
            if(hiveHoney > maxHoneyPerHive*GameManager.Instance.numOfHives){
                hiveHoney = maxHoneyPerHive*GameManager.Instance.numOfHives;
            }            
             
        }
    }

    
    // moves honey from the hive to the "inventory"
    private void collectHoney(){
        // incPlayerHoney returns any potential leftovers if the player storage is full
        int leftOvers = GameManager.Instance.incPlayerHoney(hiveHoney);
        
        resetHiveHoney();

        hiveHoney = leftOvers;
        
        
    }

    // Reset the honey in the hive to 0
    private void resetHiveHoney(){
        hiveHoney = 0;
    }
    
}
