using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public int sceneToLoad;
    private int randRoom;

    bool loaded;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (!loaded && other.tag == "Player")
        {
            // Random room logic
            int randomNum = Random.Range(1, 3+1); //not actually for 3 rooms. idk why but you have to add 1 LMAO
            randRoom = randomNum;
            sceneToLoad = randRoom;

            // Load next scene
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            loaded = true;

            // Move player to (0, 0, 0)
            PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);
        }
    }
}
