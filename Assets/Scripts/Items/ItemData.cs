using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class ItemData : ScriptableObject
{
    [Header("Name")]
    public string Name;

    [Header("Type")]
    public ItemType Type;
    public enum ItemType
    {
        Active,
        Passive,
        Consumable
    }

    [Header("Item Stats")]
    public int DropChance;
    public int Price;

    [Header("Icon")]
    public Sprite Icon;

}
