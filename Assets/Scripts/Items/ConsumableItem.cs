using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : ItemData
{

    [Header("Consumable Types")]
    public ConsumableType consumableTypes;
    public enum ConsumableType
    {
        Consumable
    }

}
