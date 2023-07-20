using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item")]
public class Item : MonoBehaviour
{
    [Header("Gameplay")]
    public Boolean stackable;
    public Image inventoryIcon;

}