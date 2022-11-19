using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwitchLogic : MonoBehaviour
{
    public string sceneToLoad;
    
    //When fully implemented the script won't need to be given a room to go to
    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.tag == "Player") {
        SceneManager.LoadScene(sceneToLoad);
    }
    }
}
