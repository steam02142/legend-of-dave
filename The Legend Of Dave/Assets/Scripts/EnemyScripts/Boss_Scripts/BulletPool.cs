using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    

    void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        // If we have active bullets
        if (bullets.Count > 0)
        {
            // Find a bullet that is not being used
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        // We dont have enough bullets in the pool, add more
        if (notEnoughBulletsInPool)
        {
            GameObject bullet = Instantiate(pooledBullet);
            bullet.SetActive(false);
            bullets.Add(bullet);
            return bullet;
        }

        // We are maxed out for bullets and all are in use
        return null;
    }    
}
