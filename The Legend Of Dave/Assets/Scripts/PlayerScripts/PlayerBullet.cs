using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 50;

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
        if (other.tag != "Player")
        {
            Destroy(gameObject);    
        }

        // If we hit enemy, damage them
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
    }

    // Destroy bullet if out of frame
    void OnBecameInvisible() 
    {
        Destroy(gameObject);    
    }
}
