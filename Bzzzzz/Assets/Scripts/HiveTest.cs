using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveTest : MonoBehaviour
{
    public Button yourButton;

    private float percentage = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    { 
    }

     void TaskOnClick()
    {
        //hiveScript.instance.setBeeProductivity(1.3f);
        GameObject.Find("Hive").GetComponent<hiveScript>().setBeeProductivity(0.9f);
        return;
    }

}