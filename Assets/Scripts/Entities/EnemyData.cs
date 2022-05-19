using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/EnemyData", order = 4)]
public class EnemyData : ScriptableObject
{
    [Header("Who is it")]
    public string Name;
    public float Height, Speed, Weight;

    [Header("Combat Stats")]
    public float Health, MaxHealth;
    public int Lifes, MaxLifes;
    public int AttackDamage;
    public float AggroRange;
    public bool Static;
    public bool Aggressive;

    [Header("On Death")]
    public int XP;
    public int Score;
    public GameObject[] DropList;

    public void ResetStats()
    {
        if (Health < MaxHealth) Health = MaxHealth;
        if (Lifes <= 0) Lifes = MaxLifes;
    }
}
