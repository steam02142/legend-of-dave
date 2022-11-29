using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public int damage = 50;
    public int bulletForce = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If the bullet hits a something, destroy it
    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            //if it hits a enemy it applies a force to said enemy and does damage
            case "Enemy":
            other.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);
            Vector3 bulletdir = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletdir * bulletForce);
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
            Destroy(gameObject);
            break;
        }
    }

    // Destroy bullet if out of frame
    void OnBecameInvisible() 
    {
        Destroy(gameObject);    
    }
}
