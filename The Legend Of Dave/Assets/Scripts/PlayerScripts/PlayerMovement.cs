using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Controls Players speed
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    //holds player x and y position
    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //getting user directional input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //make diagonal speed not 1.4x faster
        movement = movement.normalized;

        //movement animations
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //making player face the direction of the mouse (yay)
        faceMouse();
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //if the mouse is to the left of the player it'll face left
        if(mousePosition.x - transform.position.x >= 0){
            rb.transform.localScale = new Vector3(1,1,1); 
        //else it'll face right
        }else{
            rb.transform.localScale = new Vector3(-1,1,1);
        }
    }
}

