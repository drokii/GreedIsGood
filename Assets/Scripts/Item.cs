using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    public Boolean stackable;
    public int quantity;


}