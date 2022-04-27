using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptableObject : ActiveItem
{
    [Header("Weapon Stats")]
    public int Damage;
    public int CurrentBullets;
    public int MaxReloadBullets;
    public int TotalBulletsLeft;
    public int MaxCapacity;

    public float FireRate;
    public float FireForce;
    public int BulletXShoot;

    public float ReloadTime;
    public bool Reloading;
    public bool automaticReload;

    public GameObject BulletType;
}
