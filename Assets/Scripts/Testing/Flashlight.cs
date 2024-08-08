using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public void FlashOn(float battery, Light flashlight)
    {
        flashlight.intensity = 150;
        battery -= 1;
        Debug.Log(battery);
        if(battery <= 0) FlashOff(flashlight);
    }

    public void FlashOff(Light flashlight)
    {
        flashlight.intensity = 0;
    }
}
