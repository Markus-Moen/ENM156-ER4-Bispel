using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{
    public GameObject canvas;

    public void openInfo()
    {
        canvas.SetActive(true);
    }
    public void closeInfo()
    {
        canvas.SetActive(false);
    }
}
