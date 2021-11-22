using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void RestartGame() {
             Destroy(GameManager.instance);
             SceneManager.LoadScene("GameView");//(SceneManager.GetActiveScene().name); // loads current scene
         }
}
