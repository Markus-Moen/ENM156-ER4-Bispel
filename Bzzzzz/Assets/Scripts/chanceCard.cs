using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chanceCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RandomizeChanceCard()
    {
        int card = Random.Range(0, 9);
        Debug.Log("card " + card);
    }
}
