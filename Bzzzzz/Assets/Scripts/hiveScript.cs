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
    public int delay = 2;                   //Used for increasing honey
    public int honeyAmount = 0;             //The amount of honey in the hive
    private float beeProductivity = 1f;
    private float totalPercentalChange = 1f;
    //public float productivityApproximation = 1;
    //Giving away honey
    int temporaryHoney = 0;                 //Used when moving honey to the cellar
    private int leftovers = 0;
    //Hives
    private int maxHoneyPerHive = 200;     //Max amount of honey per hive
    private int maxHoney = 0;               //Total max honey across all hives
    private int tmpHives;
    private int tmpFlowHives;
    private int totalHives;
    private int normalHoneyIncrease;
    private int hiveApproximation = 0;
    //Flow hive (Upgrades all hives for now...)
    private bool ownFlowHive = false;


    Text honeyCounter;
    Text normalText;
    Text flowText;

    public static hiveScript instance;

    //I used Awake because when I gave the value to beeAmount outside Awake it did not get the right value when changed
    void Awake(){

        // Makes Hive easily available from other scripts
        if (instance != null && instance != this)
        { Destroy(this.gameObject);}
        else {instance = this;}

        tmpBees = GameManager.instance.getPlayerBees();
        
        tmpHives = GameManager.instance.getPlayerHives();
        tmpFlowHives = GameManager.instance.getPlayerFlowHives();
        totalHives = tmpHives + tmpFlowHives;
        setHoneyInc(tmpBees);
        setMaxHoney(totalHives);

        honeyCounter = GameObject.Find("HoneyCounter").GetComponent<Text>();
        flowText = GameObject.Find("FlowHoney").GetComponent<Text>();
        normalText = GameObject.Find("NormalHoney").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        //Sets the delay based on the number of bees
        tmpBees = GameManager.instance.getPlayerBees();
        setHoneyInc(tmpBees);
        normalText.text = "Honey/Normal hive: " + honeyIncrease;
        flowText.text = "Honey/Flowhive: 0";
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
        tmpHives = GameManager.instance.getPlayerHives();
        tmpFlowHives = GameManager.instance.getPlayerFlowHives();
        totalHives = tmpHives + tmpFlowHives;
        if(totalHives  != hiveApproximation){
            setMaxHoney(totalHives);
        }
        normalText.text = "Honey/Normal hive: " + normalHoneyIncrease;
        flowText.text = "Honey/Flowhive: " + (honeyIncrease - normalHoneyIncrease);
        incHoney();
        honeyCounter.text = "Honey to collect: " + honeyAmount;
    }

     void TaskOnClick()
    {
        //Moves honey to the "cellar", if the "cellar" is full, the leftovers are stored in the hive
            leftovers = GameManager.instance.incPlayerHoney(honeyAmount);
            GameManager.instance.decPlayerFood(honeyAmount/4);
            honeyAmount = leftovers;

    }
    //Increasing the honey in the hive
    void incHoney(){
        //Check if the delay time is passed
        if(timer >= delay){
            //Reset the timer
            timer = 0;
            //If the bee hive is full we return without increasing
            //If the increse in honey exceeds max honey per bee hive we set the value to max
            temporaryHoney = honeyIncrease + honeyAmount; 
            //Honey in normal hives
            normalHoneyIncrease = Mathf.RoundToInt((honeyIncrease * tmpHives/totalHives));         
            leftovers = GameManager.instance.incPlayerHoney(honeyIncrease - normalHoneyIncrease);
            GameManager.instance.decPlayerFood(honeyIncrease - normalHoneyIncrease);
            honeyAmount += leftovers;
            temporaryHoney = honeyAmount + normalHoneyIncrease;
            if(temporaryHoney > maxHoney){
                honeyAmount = maxHoney;
            }else{
                honeyAmount += normalHoneyIncrease;
            }
        }
    }

    //Function that sets how much honey we get per unit of time
    public void setHoneyInc(int numberOfBees){
        honeyIncrease = (int) (beeProductivity*numberOfBees*0.25);
        beeApproximation = numberOfBees;
        normalHoneyIncrease = Mathf.RoundToInt((honeyIncrease * tmpHives/totalHives));   
        return;
    }

    public void setHoneyIncProductivity(float prod){
        honeyIncrease = (int) (prod*honeyIncrease);
        //normalHoneyIncrease = Mathf.RoundToInt((honeyIncrease * tmpHives/totalHives));   
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
        return;
    }
    public void setFlowerProductivity(float productivity, int flowers)
    {
        float prodChange = 1f;
        for (int i = 0; i < flowers; i++)
        {
            prodChange *= productivity;
        }
        honeyIncrease = (int)(beeProductivity * beeApproximation * 0.25 * prodChange);
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
