using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet: MonoBehaviour
{
    public float bulletSpeed;
    public int damage;
    private Vector3 playerDirection;
    public GameObject hitEffect;

    private int spawnedRoom;

    private int currentRoom;


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

        //Save which scene bullet spawned in, so we know to delete it when we switch rooms
        spawnedRoom = SceneManager.GetSceneAt(1).buildIndex;
        currentRoom = SceneManager.GetSceneAt(1).buildIndex;

        //Scale Bullet Speed. Max is 1.5x default speed in room 25
        bulletSpeed = (float)(4 + (PlayerStats.instance.difficultyFactor * 0.08));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * bulletSpeed * Time.deltaTime;

        currentRoom = SceneManager.GetSceneAt(1).buildIndex;
        if (spawnedRoom != currentRoom) 
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

            case "Enemy":
            break;

            case "Bullet":
            break;

            default:
            GameObject effect2 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect2, 0.5f);
            Destroy(gameObject);
            break;
        }
    }
}
