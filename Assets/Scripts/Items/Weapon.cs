using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponScriptableObject weaponData;

    private Vector2 mousePosition;

    private float aimAngle;
    private Vector2 aimDirection;
    public Transform firePoint;

    public Rigidbody2D rb;
    public Camera sceneCamera;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sceneCamera = Camera.main;

        if (weaponData != null)
        {
            weaponData.FullReload();
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = weaponData.Icon;
        }
    }

    void Update()
    {
        if (weaponData != null)
        {
            UpdateMousePosition();
            UpdateShootingAngle();
            LetItRain();
            UserAskedForResupply();
        }
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
        aimDirection = mousePosition - rb.position;
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
        Instantiate(weaponData.ShootEffect, firePoint.position, firePoint.rotation);

        GameObject projectile = Instantiate(weaponData.BulletType, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.right * weaponData.FireForce, ForceMode2D.Impulse);

        // RaycastHit2D hit = Physics2D.Raycast(firePoint.position, aimDirection);
        // Enemy enemy = hit.collider.transform.gameObject.GetComponent<Enemy>();
        // BULLET FEEDBACK
        // if (enemy != null)
        // {
        //     IncreaseScore();
        //     enemy.Damage(weaponData.Damage);
        // }
        weaponData.UseBullet();
    }

    private void IncreaseScore()
    {
        GameManager.Instance.GetComponent<ScoreManager>().IncreaseScore(1);
    }

}
