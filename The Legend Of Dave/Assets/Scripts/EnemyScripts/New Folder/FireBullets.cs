using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public static FireBullets instance;

    [SerializeField] private int bulletsAmount = 10;

    [SerializeField] private float startAngle = 90f;
    [SerializeField] private float endAngle = 270f;

    private Vector2 bulletMoveDirection;

    void Awake() 
    {
        instance = this;
    }

    public void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            // Calculate where the last bullet should go
            float bulletDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0f);
            Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

            // Get bullet from pool and shoot it
            GameObject bullet = BulletPool.instance.GetBullet();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().setMoveDirection(bulletDirection);

            // Make next bullet move over
            angle += angleStep;
        }
    }
}
