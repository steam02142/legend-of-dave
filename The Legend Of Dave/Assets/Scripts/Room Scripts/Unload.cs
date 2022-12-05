using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : MonoBehaviour
{
    
    public static Unload instance;

    public int scene;

    bool unloaded;

    static int counter = 0;

    void Awake() 
    {
        instance = this;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        counter++;
        PlayerPrefs.SetInt("Counter", counter);

        Debug.Log("this is" + PlayerPrefs.GetInt("Counter"));

        if (PlayerPrefs.GetInt("Counter") <= 1)
        {
            unloaded = true;
            
            scene = PlayerPrefs.GetInt("startScene");

            AnyManager.anyManager.UnloadScene(scene);

            Destroy(gameObject);
            
        }

        if (!unloaded && other.tag == "Player" && PlayerPrefs.GetInt("Counter") >= 2)
        {
            unloaded = true;

            scene = PlayerPrefs.GetInt("lastRoom");

            Debug.Log (scene);

            AnyManager.anyManager.UnloadScene(scene);

            Destroy(gameObject);
        }
    }

    public void resetCounter ()
    {
        counter = 0;
    }

}
