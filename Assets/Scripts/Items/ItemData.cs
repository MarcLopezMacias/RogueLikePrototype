using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Name")]
    public string Name;

    [Header("Type")]
    public ItemType Type;

    [Header("Item Stats")]
    public int DropChance;
    public int Price;

    [Header("Image")]
    public Sprite Image;

    public enum ItemType
    {
        Active,
        Passive,
        Consumable
    }
}
