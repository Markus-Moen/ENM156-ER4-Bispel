using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBees : MonoBehaviour
{
    // I guess we declare a text object here...
    Text beesText;

    // Start is called before the first frame update
    void Start()
    {
        // ...and instatntiate it here
        beesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text in the text field
        beesText.text = "Bees: " + GameManager.Instance.numOfBees;
    }
}
