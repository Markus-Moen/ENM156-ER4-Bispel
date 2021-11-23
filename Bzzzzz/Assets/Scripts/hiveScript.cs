using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiveScript : MonoBehaviour
{
    public Button yourButton;
    //Timer keeps time of the time
    private float timer = 0;
    //Should it be public or private? It keeps track of how much honey there is in the hive
    public int honeyAmount = 0;
    //Max amount of honey per hive
    private int maxHoneyPerHive = 2000;
    //The delay until the honey is increased
    public int delay = 3;
    int beeApproximation = 0;
    int tmp = 0;
    int honeyIncrease = 0;
    int temporaryHoney = 0;
    private int leftovers = 0;
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
    /*
    //The delay is set based on the number of bees (more bees => quicker honey)
    //static 
    void setDelay(int numberOfBees){
        delay = 360.0f/numberOfBees;
        beeApproximation = numberOfBees;
        return;
    }
    */
    //Function that sets how much honey we get per unit of time
    void setHoneyInc(int numberOfBees){
        honeyIncrease = numberOfBees*2;
        beeApproximation = numberOfBees;
        return;
    }
}
