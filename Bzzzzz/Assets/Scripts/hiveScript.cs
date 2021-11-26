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
    int tmpBees = 0;                            //Used for setting honey increase
    int honeyIncrease = 0;                  //Dependent on # of bees
    //Increasing the honey
    public int delay = 3;                   //Used for increasing honey
    public int honeyAmount = 0;             //The amount of honey in the hive
    private float beeProductivity = 1f;
    private float totalPercentalChange = 1f;
    //public float productivityApproximation = 1;
    //Giving away honey
    int temporaryHoney = 0;                 //Used when moving honey to the cellar
    private int leftovers = 0;
    //Hives
    private int maxHoneyPerHive = 2000;     //Max amount of honey per hive
    private int maxHoney = 0;               //Total max honey across all hives
    private int tmpHives;
    private int hiveApproximation = 0;
    //Flow hive (Upgrades all hives for now...)
    private bool ownFlowHive = false;

    public static hiveScript instance;

    //I used Awake because when I gave the value to beeAmount outside Awake it did not get the right value when changed
    void Awake(){

        // Makes Hive easily available from other scripts
        if (instance != null && instance != this)
        { Destroy(this.gameObject);}
        else {instance = this;}

        tmpBees = GameManager.instance.getPlayerBees();
        setHoneyInc(tmpBees);
        tmpHives = GameManager.instance.getPlayerHives() + GameManager.instance.getPlayerFlowHives();
        setMaxHoney(tmpHives);
    }
    // Start is called before the first frame update
    void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        //Sets the delay based on the number of bees
        tmpBees = GameManager.instance.getPlayerBees();
        setHoneyInc(tmpBees);
        //maxHoney = maxHoneyPerHive;
        
    }

    // Update is called once per frame
    void Update()
    { 
        timer += Time.deltaTime;
        tmpBees = GameManager.instance.getPlayerBees();
        if(tmpBees != beeApproximation){
            setHoneyInc(tmpBees);
        }
        tmpHives = GameManager.instance.getPlayerHives() + GameManager.instance.getPlayerFlowHives();
        if(tmpHives != hiveApproximation){
            setMaxHoney(tmpHives);
        }
        incHoney();
        if (ownFlowHive)
        {
            TaskOnClick();
        }
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
            if(honeyAmount>=maxHoney){
                return;
            }
            //If the increse in honey exceeds max honey per bee hive we set the value to max
            temporaryHoney = honeyIncrease + honeyAmount;
            if(temporaryHoney > maxHoney){
                honeyAmount = maxHoney;
            }else{
                honeyAmount += honeyIncrease;
            }
        }
    }
    //Function that sets how much honey we get per unit of time
    public void setHoneyInc(int numberOfBees){
        //honeyIncrease = (int) productivityApproximation*numberOfBees*2; // DOES NOT WORK! ProductivityApprox is not updated somehow!!!
        honeyIncrease = (int) beeProductivity*numberOfBees*2;
        beeApproximation = numberOfBees;
        return;
    }

    public void setHoneyIncProductivity(float prod){
        honeyIncrease = (int) (prod*honeyIncrease);
        //Debug.Log("Percent " + prod.ToString());
        Debug.Log("Honey increase " + honeyIncrease.ToString());
        return;
    }

    //Sets the max honey in the beehives
    void setMaxHoney(int hives){
        maxHoney = hives * maxHoneyPerHive;
        hiveApproximation = hives;
    }

    public void setBeeProductivity(float productivity){
        setHoneyIncProductivity(productivity);
        beeProductivity = productivity;
        totalPercentalChange *= productivity;
        Debug.Log("Total change is: " + totalPercentalChange.ToString());
        return;
    }

    public float getTotalPercentalChange(){
        return totalPercentalChange;
    }

    public void upgradeHive()
    {
        ownFlowHive = true;
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
