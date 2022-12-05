using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyManager : MonoBehaviour
{
    public static AnyManager anyManager;

    bool gameStart;

    int startScene = 1;

    void Awake() 
    {
        if (!gameStart)
        {
            anyManager = this;

            PlayerPrefs.SetInt("startScene", startScene);

            SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);

            gameStart = true;
        }    
    }

    public void UnloadScene (int scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload (int scene)
    {
        yield return null;

        Debug.Log(scene);

        SceneManager.UnloadSceneAsync(scene);
    }
}
