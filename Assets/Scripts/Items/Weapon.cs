using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponScriptableObject currentWeapon;

    private Vector2 mousePosition;

    private float aimAngle;
    public Transform firePoint;

    public Rigidbody2D rb;
    public Camera sceneCamera;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sceneCamera = Camera.main;

        currentWeapon.CurrentBullets = currentWeapon.MaxReloadBullets;
        currentWeapon.TotalBulletsLeft = currentWeapon.MaxCapacity;
    }

    void Update()
    {
        UpdateMousePosition();
        UpdateShootingAngle();
        LetItRain();
    }

    private void UpdateMousePosition()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateShootingAngle()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rb.rotation = aimAngle;
    }

    private void LetItRain()
    {
        if(Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        if(HasBullets() && !currentWeapon.Reloading)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    private bool HasBullets()
    {
        return currentWeapon.CurrentBullets > 0;
    }

    private IEnumerator Reload()
    {
        currentWeapon.Reloading = true;
        yield return new WaitForSeconds(currentWeapon.ReloadTime);
        currentWeapon.Reloading = false;
        currentWeapon.CurrentBullets = currentWeapon.MaxReloadBullets;
    }

    private void Shoot()
    {
        Debug.Log("Player clicked and shot.");
        // SpawnBullet();
        UseBullet();
    }

    private void AutomaticReload()
    {
        if (!HasBullets())
        {
            Reload();
        }
    }

    private void UseBullet()
    {
        currentWeapon.CurrentBullets--;
        if (currentWeapon.automaticReload)
        {
            AutomaticReload();
        }
    }
}
