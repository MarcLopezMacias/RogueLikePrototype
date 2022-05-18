using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "ScriptableObjects/ConsumableData", order = 2)]
public class Consumables : ItemData
{

    public int AmountToConsume;
    public int ItemAmount;

}
