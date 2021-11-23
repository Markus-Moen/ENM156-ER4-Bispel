using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiveScript : MonoBehaviour
{
    public Button yourButton;
    private float timer = 0;                //Timer, used for increasing honey
    //Setting increase of honey
    int beeApproximation = 0;               //Used for setting honey increase
    int tmp = 0;                            //Used for setting honey increase
    int honeyIncrease = 0;                  //Dependent on # of bees
    //Increasing the honey
    public int delay = 3;                   //Used for increasing honey
    public int honeyAmount = 0;             //The amount of honey in the hive
    //Giving away honey
    int temporaryHoney = 0;                 //Used when moving honey to the cellar
    private int leftovers = 0;
    //Hives
    private int maxHoneyPerHive = 2000;     //Max amount of honey per hive
    private int maxHoney = 0;               //Total max honey across all hives

    //I used Awake because when I gave the value to beeAmount outside Awake it did not get the right value when changed
    void Awake(){
        //beeAmount = 100;
        int tmp = GameManager.instance.getPlayerBees();
    }
    // Start is called before the first frame update
    void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        //Sets the delay based on the number of bees
        tmp = GameManager.instance.getPlayerBees();
        //setDelay(tmp);   
    }

    // Update is called once per frame
    void Update()
    { 
        timer += Time.deltaTime;
        tmp = GameManager.instance.getPlayerBees();
        if(tmp != beeApproximation){
            //setDelay(tmp);
            setHoneyInc(tmp);
        }
        incHoney();
    }

     void TaskOnClick()
    {
        //Moves honey to the "cellar", if the "cellar" is full, the leftovers are stored in the hive
        leftovers = GameManager.instance.incPlayerHoney(honeyAmount);
        honeyAmount = leftovers;

    }
    //Increasing the honey in the hive
    void incHoney(){
        //Check if the delay time is passed
        if(timer >= delay){
            //Reset the timer
            timer = 0;
            //If the bee hive is full we return without increasing
            if(honeyAmount>=maxHoneyPerHive){
                return;
            }
            //If the increse in honey exceeds max honey per bee hive we set the value to max
            temporaryHoney = honeyIncrease + honeyAmount;
            if(temporaryHoney > maxHoneyPerHive){
                honeyAmount = maxHoneyPerHive;
            }else{
                honeyAmount += honeyIncrease;
            }
        }
    }
    //Function that sets how much honey we get per unit of time
    public void setHoneyInc(int numberOfBees){
        honeyIncrease = numberOfBees*2;
        beeApproximation = numberOfBees;
        return;
    }

    /*
    //The delay is set based on the number of bees (more bees => quicker honey)
    //static 
    void setDelay(int numberOfBees){
        delay = 360.0f/numberOfBees;
        beeApproximation = numberOfBees;
        return;
    }
    */
}
