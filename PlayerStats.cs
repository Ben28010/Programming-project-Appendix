using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public bool isGameOver;


    private void Start()
    {
        currentHealth = maxHealth; //sets current health to the max possible health when the program starts
        healthBar.SetMaxHealth(maxHealth); //sets the slider to the max health

        isGameOver = false;
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            TakeDamage(10); //used for tesing damage
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isGameOver = true;
        }
    }
}
