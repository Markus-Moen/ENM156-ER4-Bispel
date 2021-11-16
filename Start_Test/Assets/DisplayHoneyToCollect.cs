using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHoneyToCollect : MonoBehaviour
{

    // I guess we declare a text object here...
    Text honeyText;

    // Start is called before the first frame update
    void Start()
    {
        // ...and instatntiate it here
        honeyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Displays the text in the text field
        honeyText.text = "" + HiveStuff.Instance.hiveHoney;
    }

}
