using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public GameObject myPlayer;


    //points the gun in the direction of the mouse and makes it so 
    //the gun is always right side up
    void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if(rotationZ < -90 || rotationZ > 90)
        {

            if(myPlayer.transform.localScale.x == 1)
            {
                
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            
            }else if (myPlayer.transform.localScale.x == -1){

                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);

            }
        }
    }
}
