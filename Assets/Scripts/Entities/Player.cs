using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IKillable, IDamageable<float>, IHealable<float>
{
    public PlayerData playerData;
    private Vector2 StartingPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        playerData.BumpDamage = playerData.Height * playerData.Weight / 100;
        playerData.ResetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {

    }

    public void ResetPosition()
    {
        transform.position = StartingPosition;
    }

    // CHEEKY METHODS
    public void DecreaseLifes(int amount)
    {
        playerData.Lifes -= amount;
        if (playerData.Lifes <= 0)
        {
            GameManager.Instance.GameOver();
        } else
        {
            playerData.ResetHealth();
        }
    }

    public void IncreaseLifes(int amount)
    {
        playerData.Lifes += amount;
    }

    public void Kill()
    {
        GameManager.Instance.GetComponent<SoundController>().PlayDeathPlayer();
        DecreaseLifes(1);
    }

    public void Heal(float amountHealed)
    {
        playerData.Health += amountHealed;
        if (playerData.Health > playerData.MaxHealth) playerData.Health = playerData.MaxHealth;
    }

    public void Damage(float damageTaken)
    {
        if(!GodMode)
        {
            playerData.Health -= damageTaken;
            if (playerData.Health <= 0) Kill();
        }
    }

    public void Reset()
    {
        Start();
    }

    public void SetLocation(Transform location)
    {
        gameObject.transform.position = location.position;
    }

    public void ApplyBuff(Buffs buff)
    {
        switch (buff.Stat)
        {
            case "Health":
                playerData.IncreaseMaxHealth(buff.Amount);
                break;
            case "Damage":
                playerData.IncreaseDamage(buff.Amount);
                break;
            case "FireRate":
                gameObject.GetComponentInChildren<Weapon>().weaponData.IncreaseFireRate(buff.Amount);
                break;
        }
    }

}
