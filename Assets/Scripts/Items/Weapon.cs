using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponScriptableObject currentWeapon;

    public GameObject bullet;

    public Transform firePoint;

    public float fireForce;

    public Rigidbody2D rb;
    public Camera sceneCamera;
    private Vector2 mousePosition;

    private bool Reloading;

    private float aimAngle;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;
    }

    // Update is called once per frame
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
        if(UserClicks() && CanShoot())
        {
            Shoot();
            // UseBullet()
        }
    }

    private bool UserClicks()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool CanShoot()
    {
        if(HasBullets() && !Reloading)
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
        return currentWeapon.Bullet > 0;
    }

    private IEnumerator Reload()
    {
        Reloading = true;
        yield return new WaitForSeconds(currentWeapon.ReloadTime);
        Reloading = false;
    }

    private void Shoot()
    {

    }

    private void UseBullet()
    {

    }
}
