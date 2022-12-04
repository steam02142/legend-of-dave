using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 moveDirection;
    public float moveSpeed;

    public int damage;
    public GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        // Shoot bullet with specified speed
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    // Allows pattern scripts to set bullet direction
    public void setMoveDirection (Vector2 dir)
    {
        moveDirection = dir;
    }

    // We don't "destroy", but set the bullet to active, this saves on resources
    //  as we don't have to keep instantiating and deleting new objects
    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    // When player is hit, destroy bullet and do damange
    //    if something else is hit, destroy bullet
    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            case "Player":
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            PlayerStats.instance.DamagePlayer(damage);
            Destroy(effect, 0.5f);
            Destroy();
            break;

            case "RoomExit":
            break;

            case "Gun":
            break;

            case "Item":
            break;

            case "Bullet":
            break;

            default:
            GameObject effect2 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect2, 0.5f);
            Destroy(); 
            break;
        }
    }
}
