using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controls player speed
    public float movementSpeed;
    // Holds x and y input
    private Vector2 movement;

    public Rigidbody2D rb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get player movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Makes it so diagonal movement is not double speed
        movement.Normalize();

        
    }

    // Updates at same time as physics engine 
    void FixedUpdate() 
    {
        // Applies velocity to the rigid body using unity physics engine
        rb.velocity = movement * movementSpeed;
    }
}
