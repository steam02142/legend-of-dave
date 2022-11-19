using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int currentHealth;
    public int maxHealth;


    void Awake() 
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer (int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayerMovement.instance.gameObject.SetActive(false);
        }

        // Update Health UI
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        // Keeps player from overhealing
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update Health UI
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
