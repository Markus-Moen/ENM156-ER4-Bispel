using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public GameObject log;
    public List<int> savedCards = new List<int>();
    private int buttonIndex = 0;
    public GameObject[] logButtons;

    public static LogManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        { Destroy(this.gameObject); }
        else { instance = this; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creates an array of all objects with the "logButton" tag.
        logButtons = GameObject.FindGameObjectsWithTag("logButton");

        // Deactivates all the buttons, as nothing is logged before the game starts.
        foreach(GameObject logButton in logButtons)
        { logButton.SetActive(false); }

        // Inactivates the log while still letting it get instantiated.
        log.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeLog()
    {
        log.SetActive(false);
    }
    public void openLog()
    {
        log.SetActive(true);
    }

    public void saveToLog(int card)
    {
        // Saves the card int to savedCards, activates the buttons for it,
        // and increments the button list index
        savedCards.Add(card);
        logButtons[buttonIndex].SetActive(true);
        buttonIndex++;
        Debug.Log("Logged card " + string.Join(",", savedCards));
    }

    public void buttonClick(int card)
    {
        Debug.Log("Button clicked: " + savedCards[card]);
        Debug.Log("All cards: " + string.Join(",", (object[])CardManager.instance.chanceCards));
        
            // Works in theory, but chanceCards appears to be empty from here.
            // Will have to ask chance card people how the array is populated.
        //CardManager.instance.chanceCards[savedCards[card]].SetActive(true);
    }
}
