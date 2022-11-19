using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
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
        transform.position += playerDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            PlayerStats.instance.DamagePlayer();
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
