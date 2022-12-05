using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : MonoBehaviour
{
    public int scene;

    bool unloaded;

    static int counter = 0;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        counter++;

        if (counter <= 1)
        {
            unloaded = true;
            
            scene = PlayerPrefs.GetInt("startScene");

            AnyManager.anyManager.UnloadScene(scene);

            Destroy(gameObject);
            
        }

        if (!unloaded && other.tag == "Player" && counter >= 2)
        {
            unloaded = true;

            scene = PlayerPrefs.GetInt("lastRoom");

            Debug.Log("did delete" + scene);

            AnyManager.anyManager.UnloadScene(scene);

            Destroy(gameObject);
        }
        PlayerStats.instance.updateExit();
    }
}
