using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthBar;
    public Text healthText;

    public Text coinText;

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
}
