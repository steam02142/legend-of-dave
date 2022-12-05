using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    public float sightRange;
    private Vector2 moveDirection;

    public Animator animator;

    public int health = 100;

    //bool for if the player is facing right (saving on resources)
    bool facingRight = true;

    // Vars for shooting
    public bool doesShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootRange;

    public SpriteRenderer enemy;

    public float minRange;

    public float activationRange;

    public bool activated;

    public int touchDamage;

    // Start is called before the first frame update
    void Start()
    {
        health = 100 * PlayerStats.instance.difficultyFactor;
        activated = false;
        touchDamage = 1 + Mathf.RoundToInt((float)(0.2 * PlayerStats.instance.difficultyFactor));
    }

    // Update is called once per frame
    void Update()
    {   
        if (activated) {
            //do nothing
        } else {
            //Check if enemy should be awoken
            if (Vector2.Distance (transform.position, PlayerMovement.instance.transform.position) < activationRange) {
                activated = true;
            }
        }
        // If the player is within enemy sight follow the player
        if (Vector2.Distance(transform.position, PlayerMovement.instance.transform.position) > minRange)
        {
            if (PlayerMovement.instance.gameObject.activeInHierarchy)
            {
                // If the player is within enemy sight follow the player
                if (Vector2.Distance(transform.position, PlayerMovement.instance.transform.position) > minRange)
                {
                    moveDirection = PlayerMovement.instance.transform.position - transform.position;
                }
                else 
                {
                    // if you don't see the player, set movement to zero
                    moveDirection = Vector2.zero;
                }

                // Keep diagonal speed from increasing
                moveDirection.Normalize();

                rb.velocity = moveDirection * moveSpeed;

               

                Animations();
            }
        }

         if (doesShoot && Vector2.Distance(transform.position, PlayerMovement.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;
            
            if (fireCounter <=0)
            {
                fireCounter = fireRate;
                Instantiate (bullet, firePoint.position, firePoint.rotation);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") {
            PlayerStats.instance.DamagePlayer(touchDamage);
        }
    }

    public void DamageEnemy (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerStats.instance.coinPickup(1);
            UIController.instance.coinText.text = "Coins: " + PlayerStats.instance.coins.ToString();
            Destroy(gameObject);
        }
    }

















    // -----------------------------------------------------------------------------------------------------
    // Dealing with animations below here. Might change into a different script. Using this as a divider for now
    //  to keep things looking clean

    void Animations ()
    {
        // Flip enemy to face player
        if (moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }

        // Move Between idle and moving animation
        if (moveDirection == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
