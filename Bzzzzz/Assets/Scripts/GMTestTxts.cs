using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMTestTxts : MonoBehaviour
{

    Text instruction;
    // Start is called before the first frame update
    void Start()
    {
        instruction = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeTxt(string newTxt){
        instruction.text = newTxt;
    }
}
