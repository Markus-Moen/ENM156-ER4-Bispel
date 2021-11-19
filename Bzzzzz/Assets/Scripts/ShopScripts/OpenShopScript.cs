using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenShopScript : MonoBehaviour
{
	public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShop(){
    	shop.SetActive(true);
    }
}
