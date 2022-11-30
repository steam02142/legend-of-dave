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

    private GameObject[] enemyCheck;

    private Collider2D badcollider = PlayerStats.instance.collider1;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other == badcollider) {
            Debug.Log("bad collider detected BEEEEP BOOP");
            return;
        }
        enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        if (!loaded && other.tag == "Player" && enemyCheck.Length == 0)
        {
            PlayerPrefs.SetInt("lastRoom", prevRoom);
            // Random room logic
            int randomNum = UniqueRandomInt(2, 15+1); //Should be to 15+1 atm. //can change for testing purposes
            randRoom = randomNum;
            sceneToLoad = randRoom;


            // Load next scene
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            

            // Move player to (0, 0, 0)
            PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);

            //Set difficulty increase upon going to new room
            PlayerStats.instance.difficultyFactor += 1;
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
