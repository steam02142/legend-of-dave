using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiral : MonoBehaviour
{
    public static FireSpiral instance;

    private float angle = 0f;

    public float angleBetweenShots = 20f;

   void Awake() 
    {
        instance = this;
    }

    public void Fire()
    {
        
        // Calculate where the last bullet should go
        float bulletDirectionY = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulletDirectionX = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        // Calculate current bullet direction
        Vector3 bulletMoveVector = new Vector3(bulletDirectionY, bulletDirectionX, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        // Get bullet from pool and shoot it
        GameObject bullet = BulletPool.instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation =  transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().setMoveDirection(bulletDirection);

        // Make next bullet move over
        angle += angleBetweenShots;
    
    }
}
