using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{

    private int Coins;

    // Start is called before the first frame update
    void Start()
    {
        Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCoins(int value)
    {
        Coins += value;
    }

    public void DecreaseCoins(int value)
    {
        Coins -= value;
    }

    public int GetCoins()
    {
        return Coins;
    }
}
