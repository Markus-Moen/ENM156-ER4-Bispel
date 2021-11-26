using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public GameObject log;
    public List<int> savedCards = new List<int>();

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
        // Inactivates the log while still letting it get instantiated.
        //log.SetActive(true);
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
        savedCards.Add(card);
        Debug.Log("Infocards: " + string.Join(",", savedCards));
    }

    public void buttonClick(int card)
    {
        //CardManager.instance.chanceCards[savedCards[card]].SetActive(true);
        Debug.Log("Button clicked");
    }
}
