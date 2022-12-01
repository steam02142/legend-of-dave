using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossAction[] actions;
    private int currentAction;
    private float actionDuration;

    private float shotCounter;
    public Rigidbody2D rb2d;
    private Vector2 moveDirection;

    public Animator animator;

    public int health = 100;

    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;


    //bool for if the player is facing right (saving on resources)
    bool facingRight = true;

    void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100 * PlayerStats.instance.difficultyFactor;

        // Set how long the current action will take
        actionDuration = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        // If we are currently in an action
        if (actionDuration > 0)
        {
            // Decrease current action duration once per second
            actionDuration -= Time.deltaTime;

            // Start with movement at zero
            moveDirection = Vector2.zero;

            // If we should move
            if (actions[currentAction].shouldMove)
            {
                // Chase the player
                if (actions[currentAction].shouldChasePlayer)
                {
                    moveDirection = PlayerMovement.instance.transform.position - transform.position;
                    moveDirection.Normalize();
                }

                // Patrol between set points
                if (actions[currentAction].shouldPatrol)
                {
                    moveDirection = actions[currentAction].patrolPointToMoveTo.position - transform.position;
                }
            }

            // Perform movement
            rb2d.velocity = moveDirection * actions[currentAction].moveSpeed;

            // Shooting
            if (actions[currentAction].shouldShoot)
            {
                // Rotates projectile loactions based on boss direction (left/right)
                shootDirection();
                // Keep track of shooting speed
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    // For each shooting point, fire a bullet
                    foreach(Transform transform in actions[currentAction].shootingPoints)
                    {
                        Instantiate(actions[currentAction].bullet, transform.position, transform.rotation);
                    }
                }
            }
        }
        else 
        {
            // Move to next action
            currentAction++;

            // If we are at the end of our actions, reset
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            // Set action time (in seconds)
            actionDuration = actions[currentAction].actionLength;
        }

        Animations();
    }

    public void DamageEnemy (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    // Rotates projectile loactions based on boss direction (left/right)
    void shootDirection ()
    {
        if (!facingRight)
        {
            point1.transform.rotation = Quaternion.Euler(0, 0, -180);
            point2.transform.rotation = Quaternion.Euler(0, 0, 0);
            point3.transform.rotation = Quaternion.Euler(0, 0, -270);
            point4.transform.rotation = Quaternion.Euler(0, 0, -90);

        }
        else
        {
            point1.transform.rotation = Quaternion.Euler(0, 0, 0); 
            point2.transform.rotation = Quaternion.Euler(0, 0, -180);
            point3.transform.rotation = Quaternion.Euler(0, 0, -270);
            point4.transform.rotation = Quaternion.Euler(0, 0, -90);

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

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;
    public bool shouldPatrol;
    public Transform patrolPointToMoveTo;

    

    public bool shouldShoot;
    public GameObject bullet;
    public float timeBetweenShots;
    public Transform[] shootingPoints;
}