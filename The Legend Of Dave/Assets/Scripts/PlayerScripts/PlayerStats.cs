using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   [Header ("Health")]
    public static PlayerStats instance;

    public int currentHealth;
    public int maxHealth;

    [Header ("Other")]
    public int difficultyFactor;

    public CapsuleCollider2D collider1;

    public GameObject roomExit;

    public Load scriptLoad;

    public Vector3 exitLocation;

    [Header ("iFrames")]
    public float iFrames;
    public float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Currency")]
    public int coins;

    void Awake() 
    {
        instance = this;
        spriteRend = GetComponent<SpriteRenderer>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        difficultyFactor = 1; //Set baseline difficulty factor

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

        if (currentHealth > 0){
            StartCoroutine(Invunerability());  
        }
        else
        {
            PlayerMovement.instance.gameObject.SetActive(false);
            //play dying animation
            //Disabling for now 

            UIController.instance.deathScreen.SetActive(true);
        }
        // Update Health UI
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    private IEnumerator Invunerability(){
        Physics2D.IgnoreLayerCollision(8,10, true);
        Physics2D.IgnoreLayerCollision(8,11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(0.4f, 0.4f, 0.4f, 0.5f);
            yield return new WaitForSeconds(iFrames / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrames / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8,10, false);
        Physics2D.IgnoreLayerCollision(8,11, false);

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

    public void coinPickup (int amount)
    {
        coins += amount;
    }

    public void spendCoins (int amount)
    {
       coins -= amount; 

       if (coins < 0)
       {
            coins = 0;
       }
    }

    public void resetPlayer ()
    {
        currentHealth = maxHealth;
    }
}
