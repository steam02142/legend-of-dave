using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet: MonoBehaviour
{
    public float bulletSpeed;
    public int damage;
    private Vector3 playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Done in start otherwise bullet would follow player
        playerDirection = PlayerMovement.instance.transform.position - transform.position;
        playerDirection.Normalize();
        //Adjust to Scale enemy bullet damage
        //Currently difficulty Factor is a linear increase of 1 each room so in this case
        //would change enemy bullet damage by +1 every ~5 rooms
        damage = 1 + Mathf.RoundToInt((float)(0.2 * PlayerStats.instance.difficultyFactor));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            case "Player":
            PlayerStats.instance.DamagePlayer(damage);
            Destroy(gameObject);
            break;

            case "RoomExit":
            break;

            case "Gun":
            break;

            case "Item":
            break;

            default:
            Destroy(gameObject);
            break;
        }
    }
}
