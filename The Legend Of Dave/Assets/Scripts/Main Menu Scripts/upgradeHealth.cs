using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeHealth : MonoBehaviour
{
    public void upgrade() {
        PlayerStats.instance.upgradeHealth();
    }
}
