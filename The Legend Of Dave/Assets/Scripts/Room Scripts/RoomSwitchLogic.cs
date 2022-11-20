using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitchLogic : MonoBehaviour
{
    public string sceneToLoad;
    private string s;
    
    //When fully implemented the script won't need to be given a room to go to
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.tag == "Player") {
        int randomNum = Random.Range(1, 3+1); //not actually for 3 rooms. idk why but you have to add 1 LMAO
        s = randomNum.ToString();
        sceneToLoad = "Room" + s;
        SceneManager.LoadScene(sceneToLoad);
    }
    }
}
