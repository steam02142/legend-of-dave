using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeDamage : MonoBehaviour
{
    public void upgrade() {
        PlayerStats.instance.upgradeDamage();
    }
}
