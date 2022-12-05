using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public int damage = 50;
    public int bulletForce = 500;
    public GameObject hitEffect;


    // If the bullet hits a something, destroy it
    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            //if it hits a enemy it applies a force to said enemy and does damage
            case "Enemy":
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<EnemyController>().DamageEnemy(damage + (PlayerStats.instance.damageUpsBought * 50));
            Vector3 bulletdir = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletdir * bulletForce);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
            break;
            
            case "RoomExit":
            break;

            case "Player":
            break;

            case "Gun":
            break;

            case "Item":
            break;
            
            default:
            GameObject effect2 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect2, 0.5f);
            Destroy(gameObject);
            break;
        }
    }
}
