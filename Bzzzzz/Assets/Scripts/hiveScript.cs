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
    //The delay until the honey is increased
    public float delay = 0;
    int beeApproximation = 0;
    int tmp = 0;
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
        setDelay(tmp);   
    }

    // Update is called once per frame
    void Update()
    { 
        timer += Time.deltaTime;
        incHoney();
        tmp = GameManager.instance.getPlayerBees();
        if(tmp != beeApproximation){
            setDelay(tmp);
        }
    }

     void TaskOnClick()
    {
        //Fill out for increasing the honey in the "bank"
        GameManager.instance.incPlayerHoney(honeyAmount);
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
    //static 
    void setDelay(int numberOfBees){
        delay = 360.0f/numberOfBees;
        beeApproximation = numberOfBees;
        return;
    }
}
