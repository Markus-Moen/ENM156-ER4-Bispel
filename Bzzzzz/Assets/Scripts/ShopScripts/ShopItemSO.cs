using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "ShopMenu", menuName = "Scriptable Object/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public int baseCost;
    public int owned;
    public string description;
}
