using System;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField] private Boolean stackable;
    [SerializeField] private Sprite inventoryIcon;
    [SerializeField] private string name;
    [SerializeField] private Guid id = Guid.NewGuid();
    [SerializeField] private GameObject prefab;

    public int quantity = 1;

    public bool Stackable { get => stackable; }
    public Sprite InventoryIcon { get => inventoryIcon; }
    public string Name { get => name; }

    public string Id { get => id.ToString(); }
    public GameObject Prefab { get => prefab; }
}