using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
	public GameObject canvas;
	public void Start(){
		Time.timeScale = 0;
	}
	public void startGame()
	{
		canvas.SetActive(false);
		Time.timeScale = 1;
	}
    // Start is called before the first frame update
    
}
