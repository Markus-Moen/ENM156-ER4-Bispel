using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHives : MonoBehaviour
{
    // I guess we declare a text object here...
    Text hivesText;

    // Start is called before the first frame update
    void Start()
    {
        // ...and instatntiate it here
        hivesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text in the text field
        hivesText.text = "Hives: " + GameManager.Instance.numOfHives;
    }
}
