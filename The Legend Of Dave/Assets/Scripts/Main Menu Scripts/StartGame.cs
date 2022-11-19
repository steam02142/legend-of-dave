using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private string sceneToLoad;
    private string s;
    public void start () {
        int randomNum = Random.Range(1, 2+1); //not actually for 3 rooms. idk why but you have to add 1 LMAO
        s = randomNum.ToString();
        sceneToLoad = "Room" + s;
        SceneManager.LoadScene(sceneToLoad);
    }
}