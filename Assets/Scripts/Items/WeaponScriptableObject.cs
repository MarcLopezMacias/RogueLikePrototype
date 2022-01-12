using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptableObject : ActiveItem
{
    public int Damage;
    public int Bullet;
    public int MaxCapacity;

    public float FireRate;
    public int BulletXShoot;

    public float ReloadTime;
}
