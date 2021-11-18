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
    Text foodText;
    Text honeyText;
    Text hivesText;
    Text beesText;
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

        // Initialize text fields
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
        honeyText = GameObject.Find("Honey Text").GetComponent<Text>();
        hivesText = GameObject.Find("Hives Text").GetComponent<Text>();
        beesText = GameObject.Find("Bees Text").GetComponent<Text>();

        // Set text field texts
        foodText.text = "Food: " + food;
        honeyText.text = "Honey: " + honey;
        hivesText.text = "Hives: " + hives;
        beesText.text = "Bees: " + bees;

        //foodTxt.changeTxt("Food: " + food);
        //honeyTxt.changeTxt("Honey: " + honey);
        //hivesTxt.changeTxt("Hives: " + hives);
        //beesTxt.changeTxt("Bees: " + bees);
    }

    // Update is called once per frame
    void Update()
    {
        // Only update if something has changed


        if(food != GameManager.instance.getPlayerFood()){
            food = GameManager.instance.getPlayerFood();
            //foodTxt.changeTxt("Food: " + food);
            foodText.text = "Food: " + food;
        }
        if(honey != GameManager.instance.getPlayerHoney()){
            honey = GameManager.instance.getPlayerHoney();
            //honeyTxt.changeTxt("Honey: " + honey);
            honeyText.text = "Honey: " + honey;
        }
        if(hives != GameManager.instance.getPlayerHives()){
            hives = GameManager.instance.getPlayerHives();
            //hivesTxt.changeTxt("Hives: " + hives);
            hivesText.text = "Hives: " + hives;
        }
        if(bees != GameManager.instance.getPlayerBees()){
            bees = GameManager.instance.getPlayerBees();
            //beesTxt.changeTxt("Bees: " + bees);
            beesText.text = "Bees: " + bees;
        }

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
            GameManager.instance.incPlayerFood(n);
    }
    public void testincPlayerHives(int n){
            GameManager.instance.incPlayerHives(n);
    }
    public void testincPlayerHoney(int n){
            GameManager.instance.incPlayerHoney(n);
    }


    


}
