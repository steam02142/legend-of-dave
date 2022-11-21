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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            case "Player":
                PlayerStats.instance.DamagePlayer(damage);
                Destroy(gameObject);
                break;
        }
    }
}
