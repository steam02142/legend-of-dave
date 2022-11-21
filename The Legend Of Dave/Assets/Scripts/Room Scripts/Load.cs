using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public int scene;

    bool loaded;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (!loaded && other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            loaded = true;

            PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);
        }
    }
}
