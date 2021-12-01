using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public GameObject log;
    private List<GameObject> savedCards = new List<GameObject>();
    public GameObject[] logButtons;
    private int buttonIndex = 0;

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

    public void saveToLog(GameObject card)
    {
        // Saves the card int to savedCards if it's not there already,
        // activates the buttons for it, and increments the button list index
        //Debug.Log("Saved? " + savedCards.Contains(card));
        //Debug.Log("Card name is " + card.name.Contains("Card"));
        if (!savedCards.Contains(card)) {
            savedCards.Add(card);
            logButtons[buttonIndex].SetActive(true);
            logButtons[buttonIndex].GetComponentInChildren<Text>().text = infoTitles(card.name);// infoTitles[card];
            buttonIndex++;
        }
        Debug.Log("Logged card " + string.Join(",", savedCards));
    }

    // Gives the title of the card based on the number in its name.
    private string infoTitles(string name)
    {
        if      (name.Contains("1"))
        { return "So Many Bees"; }
        else if (name.Contains("2"))
        { return "Worker Bee"; }
        else if (name.Contains("3"))
        { return "Drone"; }
        else if (name.Contains("4"))
        { return "Queen Bee"; }
        else if (name.Contains("5"))
        { return "Honey!"; }
        else if (name.Contains("6"))
        { return "Danger!"; }
        else { return "????"; }
    }

    public void buttonClick(int card)
    {
        Debug.Log("Button clicked: " + savedCards[card]);
        Debug.Log("All cards: " + string.Join(",", savedCards));
        
        // Activates the card related to the button pressed.
        savedCards[card].SetActive(true);
    }
}
