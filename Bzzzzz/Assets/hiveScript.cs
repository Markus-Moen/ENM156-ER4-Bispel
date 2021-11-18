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
    private int maxHoneyPerHive = 20;
    //The number of bees in the hive
    public float beeAmount = 0;
    //The delay until the honey is increased
    public float delay = 0;
    //I used Awake because when I gave the value to beeAmount outside Awake it did not get the right value when changed
    void Awake(){
        beeAmount = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        //Sets the delay based on the number of bees
        setDelay();   
    }

    // Update is called once per frame
    void Update()
    { 
        timer += Time.deltaTime;
        incHoney();
    }

     void TaskOnClick()
    {
        //Fill out for increasing the honey in the "bank"
        honeyAmount = 0;
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
            honeyAmount++;
        }
    }
    //The delay is set based on the number of bees (more bees => quicker honey)
    void setDelay(){
        delay = 360/beeAmount;
        return;
    }
}
