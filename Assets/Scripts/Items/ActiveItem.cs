using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : ItemData
{

    [Header("Active Types")]
    public ActiveType activeTypes;
    public enum ActiveType
    {
        Armor,
        Weapon
    }

}
