using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthBar;
    public Text healthText;

    public Text coinText;

    public Text Roomcount;

    public GameObject deathScreen;

    public int newGameScene;

    void Awake() 
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make Cursor Invisable
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync(newGameScene, LoadSceneMode.Additive);

        instance.deathScreen.SetActive(false);

        int currentRoom = PlayerPrefs.GetInt("currentRoom");

        if (PlayerPrefs.GetInt("counter") == 0)
        {
            currentRoom = 1;
        }

        int currentScene = SceneManager.GetSceneAt(1).buildIndex;

        AnyManager.anyManager.UnloadScene(currentScene);

        PlayerMovement.instance.gameObject.SetActive(true);

        PlayerPrefs.SetInt("Counter", 0);

       Unload.instance.resetCounter();
       PlayerMovement.instance.transform.position = new Vector3 (0, 0, 0);


        
    }

}
