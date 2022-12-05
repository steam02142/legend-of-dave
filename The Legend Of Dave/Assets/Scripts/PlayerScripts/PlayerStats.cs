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

    [Header ("iFrames")]
    public float iFrames;
    public float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Currency")]
    public int coins;

    public int roomCount;

    [Header ("Upgrade Variables")]

    public int[] healthUpgradeCost;

    public int[] damageUpdateCost;

    public int[] speedUpdateCost;

    public int healthUpsBought;

    public int damageUpsBought;

    public int speedUpsBought;

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

        roomCount = 0;

        //Set upgrade costs
        healthUpgradeCost = new int[3];
        healthUpgradeCost[0] = 25;
        healthUpgradeCost[1] = 75;
        healthUpgradeCost[2] = 150;

        damageUpdateCost = new int[3];
        damageUpdateCost[0] = 50;
        damageUpdateCost[1] = 125;
        damageUpdateCost[2] = 200;

        speedUpdateCost = new int[3];
        speedUpdateCost[0] = 40;
        speedUpdateCost[1] = 80;
        speedUpdateCost[2] = 120;

        healthUpsBought = 0;
        damageUpsBought = 0;
        speedUpsBought = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer (int damage)
    {
        currentHealth -= damage;
        // If player died
        if (currentHealth <= 0){
            //StartCoroutine(Invunerability());
            //PlayerMovement.instance.gameObject.SetActive(false);
            PlayerMovement.instance.FreezePlayer();
            Cursor.visible = true;
            //play dying animation
            //Disabling for now 
            PlayerStats.instance.currentHealth = 0;
            UIController.instance.deathScreen.SetActive(true);
            UIController.instance.healthUpgradeCost.text = healthUpgradeCost[healthUpsBought].ToString() + " Coins";
            UIController.instance.damageUpgradeCost.text = damageUpdateCost[damageUpsBought].ToString() + " Coins";
            UIController.instance.speedUpgradeCost.text = speedUpdateCost[speedUpsBought].ToString() + " Coins";
            updateDeadCoinCount();
        }
        else
        {
            StartCoroutine(Invunerability());          
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

    public void newStart() {
        currentHealth = maxHealth;

        difficultyFactor = 1; //Set baseline difficulty factor

        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        roomCount = 0;

        UIController.instance.Roomcount.text = "Room " + roomCount.ToString();

        Cursor.visible = false;
    }

    public void upgradeHealth() {
        if (healthUpsBought >= 3) {
            return;
        }
        if (healthUpgradeCost[healthUpsBought] > coins) {
            //do nothing
        } else {
            maxHealth = maxHealth * 2;
            coins = coins - healthUpgradeCost[healthUpsBought];
            healthUpsBought = healthUpsBought + 1;
            if (healthUpsBought >= 3) {
                UIController.instance.healthUpgradeCost.text = "Upgrade MAXED";
            } else {
                UIController.instance.healthUpgradeCost.text = healthUpgradeCost[healthUpsBought] + " Coins";
            }
            updateDeadCoinCount();
        }
    }

    public void upgradeDamage() {
        if (damageUpsBought >= 3) {
            return;
        }
        if (damageUpdateCost[damageUpsBought] > coins) {
            //do nothing
        } else {
            coins = coins - damageUpdateCost[damageUpsBought];
            damageUpsBought = damageUpsBought + 1;
            if (damageUpsBought >= 3) {
                UIController.instance.damageUpgradeCost.text = "Upgrade MAXED";
            } else {
                UIController.instance.damageUpgradeCost.text = damageUpdateCost[damageUpsBought] + " Coins";
            }
            updateDeadCoinCount();
        }
    }

    public void upgradeSpeed() {
        if (speedUpsBought >= 3) {
            return;
        }
        if (speedUpdateCost[speedUpsBought] > coins) {
            //do nothing
        } else {
            coins = coins - speedUpdateCost[speedUpsBought];
            speedUpsBought = speedUpsBought + 1;
            PlayerMovement.instance.moveSpeed += 1;
            if (speedUpsBought >= 3) {
                UIController.instance.speedUpgradeCost.text = "Upgrade MAXED";
            } else {
                UIController.instance.speedUpgradeCost.text = speedUpdateCost[speedUpsBought] + " Coins";
            }
            updateDeadCoinCount();
        }
    }

    public void updateDeadCoinCount() {
        UIController.instance.deadCoinCount.text = "You have " + coins.ToString() + " coins";
    }
}
