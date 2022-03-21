using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerKind
{
    Sorcerer,
    Assassin,
    Barbarian,
    Flying_Machine
}

public class Player : Character
{
    [SerializeField]
    private PlayerKind PlayerKind;

    private Vector2 StartingPosition;

    private float BumpDamage;

    [SerializeField]
    private int Level;
    [SerializeField]
    private int XP;
    [SerializeField]
    private int XPRequiredToLevelUp;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
        BumpDamage = Height * Weight / 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerKind GetKind()
    {
        return PlayerKind;
    }

    private bool IsPlayerAlive()
    {
        if (Health > 0) return true; else return false;
    }

    private void OnDestroy()
    {

    }

    public void ResetPosition()
    {
        transform.position = StartingPosition;
    }

    public void DecreaseLifes(int amount)
    {
        Lifes -= amount;
        if (Lifes <= 0)
        {
            GameManager.Instance.GameOver();
        } else
        {
            Health = MaxHealth;
            GameManager.Instance.ResetStage();
        }
    }

    public void IncreaseLifes(int amount)
    {
        if (Lifes < MaxLifes) Lifes += amount;
    }

    public void IncreaseXP(int value)
    {
        XP += value;
        CheckIncreaseLevel();
    }

    private void CheckIncreaseLevel()
    {
        if(XP >= XPRequiredToLevelUp)
        {
            IncreaseLevel();
            DecreaseXP(XPRequiredToLevelUp);
        }
    }

    private void IncreaseLevel()
    {
        Level += 1;
    }

    public void DecreaseXP(int value)
    {
        XP -= value;
    }

    public void ResetXP()
    {
        XP = 0;
    }

    public float GetBumpDamage()
    {
        return BumpDamage;
    }



}
