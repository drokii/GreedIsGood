using System;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] private Boolean stackable;
    [SerializeField] private Sprite inventoryIcon;
    [SerializeField] private string name;

    public int quantity = 1;

    public bool Stackable { get => stackable; }
    public Sprite InventoryIcon { get => inventoryIcon; }
    public string Name { get => name; }

}