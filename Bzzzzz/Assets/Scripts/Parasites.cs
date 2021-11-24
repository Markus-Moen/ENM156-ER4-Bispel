using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parasites : MonoBehaviour
{

    private bool deathByParasites = false;  // true if bees start to die because of parasites

    private const float baseKillingRate = 0.9f;             // the standard rate of killing bees
    private float parasiteKillingRate = baseKillingRate;    // the rate bees drop off

    private float timer = 0;
    private float delay = 5;        // the time (seconds) before the timer event and timer resets 

    private float randomEventTimer = 0;     // timer for random parasites
    private float randomEventDelay = 60;    // every 60 seconds there is a risk of parasites
    private int parasiteRisk = 0;           // is set to a random number. if == 1 => parasites



    //private static Parasites _instance;
    /*public static Parasites instance{ get { 
        if(_instance == null){
            GameObject go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }
        return _instance; }             
    }*/
    

    /*private void Awake(){
        if(_instance != this && _instance != null){
            Destroy(this.gameObject);
        }
        _instance = this;
    }*/

    public static Parasites instance;
    private void Awake() => instance = this;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // start killing bees if the boolean is true
        if(deathByParasites){
            timer += Time.deltaTime;
            if(timer >= delay){
                // kill a number of bees
                GameManager.instance.changeBeePercent(parasiteKillingRate);
                // reset timer
                timer = 0;
            }
        }

        // every randomEventDelay seconds there is a 10% chance of parasites
        //randomEventTimer += Time.deltaTime;           // uncomment to unleash hell (start the random parasites)
        if(randomEventTimer >= randomEventDelay){
            parasiteRisk = Random.Range(0, 10);
            if(parasiteRisk == 1){                  // if parasiteRisk == 1, the parasites start
                deathByParasites = true;
            }
        }

        
    }


    // Start killing bees
    public void startParasites(){
        deathByParasites = true;
        setParasiteKillingRate(baseKillingRate);
    }

    // start killing bees with your own personal killing rate
    public void startParasites(float f){
        deathByParasites = true;
        setParasiteKillingRate(f);
    }

    // stop killing bees
    public void stopParasites(){
        deathByParasites = false;
        setParasiteKillingRate(baseKillingRate);    // reset killing rate to base killing rate
    }

    // set the killing rate (e.g. 0.9 for 10% per time unit)
    public void setParasiteKillingRate(float f){
        parasiteKillingRate = f;
    }

    // set the time between bees dying
    public void setDelay(float f){
        delay = f;
    }

    
    

    


}
