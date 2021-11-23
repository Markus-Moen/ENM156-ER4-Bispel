using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMTestScript : MonoBehaviour
{

    // Commented code is from a bygone testing era

    //public GMTestTxts foodTxt;
    //public GMTestTxts honeyTxt;
    //public GMTestTxts hivesTxt;
    //public GMTestTxts beesTxt;
    int food;
    int honey;
    int hives;
    int bees;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the variables
        food = GameManager.instance.getPlayerFood();
        honey = GameManager.instance.getPlayerHoney();
        hives = GameManager.instance.getPlayerHives();
        bees = GameManager.instance.getPlayerBees();


        //foodTxt.changeTxt("Food: " + food);
        //honeyTxt.changeTxt("Honey: " + honey);
        //hivesTxt.changeTxt("Hives: " + hives);
        //beesTxt.changeTxt("Bees: " + bees);
    }

    // Update is called once per frame
    void Update()
    {
        // Only update if something has changed

    }


    // Test functions:
    // For button

    public void testDecPlayerBees(int n){
            GameManager.instance.decPlayerBees(n);
    }
    public void testDecPlayerFood(int n){
            GameManager.instance.decPlayerFood(n);
    }
    public void testDecPlayerHives(int n){
            GameManager.instance.decPlayerHives(n);
    }
    public void testDecPlayerHoney(int n){
            GameManager.instance.decPlayerHoney(n);
    }
    public void testincPlayerBees(int n){
            GameManager.instance.incPlayerBees(n);
    }
    public void testincPlayerFood(int n){
            int m = GameManager.instance.incPlayerFood(n);
            Debug.Log(m);
    }
    public void testincPlayerHives(int n){
            GameManager.instance.incPlayerHives(n);
    }
    public void testincPlayerHoney(int n){
            GameManager.instance.incPlayerHoney(n);
    }
    public void testchangeHoneyPercent(float f){
            GameManager.instance.changeHoneyPercent(f);
    }
    public void testSetFood(){
            GameManager.instance.setMaxFood(200);
    }
    public void testStartParasites(){
            Parasites.instance.startParasites();
    }
    public void testStopParasites(){
            Parasites.instance.stopParasites();
    }


    


}
