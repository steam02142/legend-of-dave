using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnButtonHelper : MonoBehaviour
{
    public GameObject roomExit;

    public Vector3 exitLocation;

    public void start() {
        roomExit = GameObject.Find("Triangle");
        exitLocation = roomExit.transform.position;
        PlayerStats.instance.transform.position = exitLocation;
    }
}
