using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    public int damage;
    public GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
        direction.Normalize();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Boss has been defeated, destroy bullets
        if (!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        switch(other.gameObject.tag){
            case "Player":
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            PlayerStats.instance.DamagePlayer(damage);
            Destroy(effect, 0.5f);
            Destroy(gameObject);
            break;

            case "RoomExit":
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
