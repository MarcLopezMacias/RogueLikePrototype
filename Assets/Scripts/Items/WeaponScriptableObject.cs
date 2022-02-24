using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponScriptableObject : ActiveItem
    // , IShootable<float>
{
    public int Damage;
    public int Bullet;
    public int MaxCapacity;

    public float FireRate;
    public int BulletXShoot;

    public float ReloadTime;

    public Transform firePoint;

    /*
    public void Shoot(float damage)
    {
        Vector2 direction = gameObject.GetComponent<PlayerUserMovement>().faceDirection();
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<enemy>();
            if(enemy != null)
            {
                enemy.Damage(damage);
            }
        }
    }
    */
}
