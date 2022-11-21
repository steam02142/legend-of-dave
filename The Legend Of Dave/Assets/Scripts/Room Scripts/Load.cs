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

    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (!loaded && other.tag == "Player")
        {
            PlayerPrefs.SetInt("lastRoom", prevRoom);
            // Random room logic
            int randomNum = UniqueRandomInt(2, 3+1); //not actually for 3 rooms. idk why but you have to add 1 LMAO
            randRoom = randomNum;
            sceneToLoad = randRoom;

            Debug.Log(sceneToLoad);

            

            // Load next scene
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            

            // Move player to (0, 0, 0)
            PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);
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
