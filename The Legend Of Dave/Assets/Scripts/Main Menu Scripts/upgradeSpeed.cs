using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeSpeed : MonoBehaviour
{
    public void upgrade() {
        PlayerStats.instance.upgradeSpeed();
    }
}
