using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponScriptableObject weaponData;

    private Vector2 mousePosition;

    private float aimAngle;
    public Transform firePoint;

    public Rigidbody2D rb;
    public Camera sceneCamera;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sceneCamera = Camera.main;

        if (weaponData != null)
        {
            weaponData.ResetWeapon();
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = weaponData.Icon;
        }
    }

    void Update()
    {
        UpdateMousePosition();
        UpdateShootingAngle();
        LetItRain();
        UserAskedForResupply();
    }

    private void UserAskedForResupply()
    {
        if(Input.GetButtonDown("Reload"))
        {
            weaponData.Resupply();
            Debug.Log("User asked for Ammo Resupply");
        }
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
        if (Input.GetMouseButtonDown(0) && weaponData.CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Player clicked and shot.");
        // SpawnBullet();
        weaponData.UseBullet();
    }



}
