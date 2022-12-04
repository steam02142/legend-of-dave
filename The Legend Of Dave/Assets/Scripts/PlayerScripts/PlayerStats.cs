using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int currentHealth;
    public int maxHealth;

    public int difficultyFactor;

    public CapsuleCollider2D collider1;

    public Object roomExit;
 
    void Awake() 
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        difficultyFactor = 1; //Set baseline difficulty factor

        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        updateExit();
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
            //PlayerMovement.instance.gameObject.SetActive(false);
            
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

    public void updateExit(){
        roomExit = GameObject.Find("Triangle");
        //Debug.Log("Exit has been updated");
    }
}
