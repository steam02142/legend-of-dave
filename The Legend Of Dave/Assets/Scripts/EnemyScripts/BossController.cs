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

    public ScatterShot fireBullets;
    public FireSpiral fireSpiral;

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

        actionDuration = actions[currentAction].actionLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionDuration > 0)
        {
            // Decrease current action duration once per second
            actionDuration -= Time.deltaTime;

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


            rb2d.velocity = moveDirection * actions[currentAction].moveSpeed;

            // Shooting
            if (actions[currentAction].shouldShoot)
            {
                shootDirection();
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    foreach(Transform transform in actions[currentAction].shootingPoints)
                    {
                        Instantiate(actions[currentAction].bullet, transform.position, transform.rotation);
                    }
                }
            }

            if (actions[currentAction].spiralShot)
            {
                
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    FireSpiral.instance.Fire();
                }
            }

            if (actions[currentAction].scatterShot)
            {

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    ScatterShot.instance.Fire();
                }
            }
        }
        else 
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }

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
    public bool spiralShot;
    public bool scatterShot;
    public GameObject bullet;
    public float timeBetweenShots;
    public Transform[] shootingPoints;
}