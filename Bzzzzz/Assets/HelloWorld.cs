using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HelloWorld!");
        //Debug.Log("food " + GameManager.instance.getPlayerFood());
        //Debug.Log("bees " + GameManager.instance.getPlayerBees());
        //Debug.Log("hives " + GameManager.instance.getPlayerHives());
        //Debug.Log("honey " + GameManager.instance.getPlayerHoney());
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Bzzzz");

        // Tests for GameManager
        //GameManager.instance.incPlayerFood(10);
        //Debug.Log("food " + GameManager.instance.getPlayerFood());
        //GameManager.instance.incPlayerBees(10);
        //Debug.Log("bees " + GameManager.instance.getPlayerBees());
        //GameManager.instance.incPlayerHives(10);
        //Debug.Log("hives " + GameManager.instance.getPlayerHives());
        //GameManager.instance.incPlayerHoney(10);
        //Debug.Log("honey" + GameManager.instance.getPlayerHoney());

    }
}
