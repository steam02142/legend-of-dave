using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : MonoBehaviour
{
    public int scene;

    bool unloaded;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!unloaded && other.tag == "Player")
        {
            unloaded = true;

            AnyManager.anyManager.UnloadScene(scene);
        }
        
    }
}
