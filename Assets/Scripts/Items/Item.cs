using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string Name;
    public int DropChance;
    public int Price;
    public ItemType Type;
    public Sprite Image;

    public enum ItemType
    {
        Active,
        Passive,
        Consumable
    }
}
