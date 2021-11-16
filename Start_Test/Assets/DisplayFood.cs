using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFood : MonoBehaviour
{
    // I guess we declare a text object here...
    Text foodText;

    // Start is called before the first frame update
    void Start()
    {
        // ...and instatntiate it here
        foodText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text in the text field
        foodText.text = "Food: " + GameManager.Instance.numOfFood;
    }
}
