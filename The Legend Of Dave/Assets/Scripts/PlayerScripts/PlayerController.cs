using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Controls player speed
    public float movementSpeed;
    // Holds x and y input
    private Vector2 moveInput;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Makes it so diagonal movement is not double speed
        moveInput.Normalize();

        rb.velocity = moveInput * movementSpeed;
    }
}
