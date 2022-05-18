using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObjects/PlayerData", order = 99)]
public class PlayerData : ScriptableObject
{
    [Header("Who is it")]
    public string Name;
    public float Height, Speed, Weight;
    public PlayerKind Kind;
    public enum PlayerKind
    {
        Sorcerer,
        Assassin,
        Barbarian,
        Flying_Machine
    }

    [Header("Stats")]
    public int Level;
    public int XP;
    public int XPRequiredToLevelUp;

    [Header("Combat Stats")]
    public float BumpDamage;
    public float Health, MaxHealth;
    public int Lifes, MaxLifes;

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
        if (XP >= XPRequiredToLevelUp)
        {
            IncreaseLevel();
            DecreaseXP(XPRequiredToLevelUp);
        }
    }

    public void DecreaseXP(int value)
    {
        XP -= value;
    }

    public void ResetXP()
    {
        XP = 0;
    }

    private void IncreaseLevel()
    {
        Level += 1;
    }

    private bool IsPlayerAlive()
    {
        if (Health > 0) return true; else return false;
    }

    public void ResetPlayerData()
    {
        ResetHealth();
        ResetLifes();
    }

    private void ResetHealth()
    {
        Health = MaxHealth;
    }

    private void ResetLifes()
    {
        Lifes = MaxLifes;
    }

}
