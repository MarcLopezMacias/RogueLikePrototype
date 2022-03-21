using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptableObject : ActiveItem
{
    [Header("Weapon Stats")]
    public int Damage;
    public int Bullet;
    public int MaxCapacity;

    public float FireRate;
    public int BulletXShoot;

    public float ReloadTime;
}
