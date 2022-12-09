using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public int sceneToLoad;
    int randRoom;

    bool loaded;
    
    static int prevRoom;

    private GameObject[] bossCheck;

    private GameObject[] enemyCheck;

    private Collider2D badcollider;

    static public int counter = 0;

    int roomsToBoss = 24;

    private void Start() 
    {
        badcollider = PlayerStats.instance.collider1;
        counter = 0;
        PlayerPrefs.SetInt("counter", counter);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other == badcollider) {
            return;
        }
        if (counter >= 1) {
            return;
        }
        enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        bossCheck = GameObject.FindGameObjectsWithTag("Boss");

        if (!loaded && other.tag == "Player" && enemyCheck.Length == 0 && bossCheck.Length == 0)
        {
            PlayerPrefs.SetInt("lastRoom", prevRoom);
            // Random room logic
            if (PlayerStats.instance.roomCount != roomsToBoss)
            {

            int randomNum = UniqueRandomInt(2, 15+1); //Should be to 15+1 atm. //can change for testing purposes
            randRoom = randomNum;
            sceneToLoad = randRoom;
            }
            else 
            {
                prevRoom = 16;
                sceneToLoad = 16;
            }


            PlayerPrefs.SetInt("currentRoom", sceneToLoad);
            // Load next scene
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            

            // Move player to (0, 0, 0)
            PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);

            //Set difficulty increase upon going to new room
            PlayerStats.instance.difficultyFactor += 1;

            counter += 1;
            PlayerStats.instance.roomCount += 1;
            UIController.instance.Roomcount.text = "Room " + PlayerStats.instance.roomCount.ToString();
        }
    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while(prevRoom == val)
        {
            val = Random.Range(min, max);
        }
        prevRoom = val;
        return val;
    }



}
