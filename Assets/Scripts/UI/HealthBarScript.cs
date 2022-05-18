using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image HealthBar;
    private float CurrentHealth;
    private float MaxHealth;
    Player PlayerController;

    void Start()
    {
        HealthBar = GetComponent<Image>();
        PlayerController = GameManager.Instance.Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController == null)
        {
            Start();
        }
        CurrentHealth = PlayerController.playerData.Health;
        MaxHealth = PlayerController.playerData.MaxHealth;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        
        // Debug.Log("%: " + CurrentHealth / MaxHealth + "Current: " + CurrentHealth + ". Max: " + MaxHealth);
    }
}
