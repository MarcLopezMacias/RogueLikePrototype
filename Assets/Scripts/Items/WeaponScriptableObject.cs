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

    public GameObject BulletType;

    public bool CanShoot()
    {
        return HasBullets(); // && !Reloading;
    }

    public bool HasBullets()
    {
        return CurrentBullets > 0;
    }

    public void UseBullet()
    {
        CurrentBullets--;
    }

    public void ResetWeapon()
    {
        CurrentBullets = MaxReloadBullets;
        TotalBulletsLeft = MaxCapacity;
    }

    public void Resupply()
    {
        if(CanReload())
        {
            TotalBulletsLeft -= (MaxReloadBullets - CurrentBullets);
            CurrentBullets = MaxReloadBullets;
        } 
        else
        {
            CurrentBullets += TotalBulletsLeft;
            TotalBulletsLeft = 0;
        }
    }

    private bool CanReload()
    {
        return TotalBulletsLeft - (MaxReloadBullets - CurrentBullets) >= 0;
    }
}
