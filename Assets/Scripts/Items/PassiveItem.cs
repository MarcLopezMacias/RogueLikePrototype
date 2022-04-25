using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{

    [Header("Passive Types")]
    public PassiveType passiveType;
    public enum PassiveType
    {
        Buff
    }

}
