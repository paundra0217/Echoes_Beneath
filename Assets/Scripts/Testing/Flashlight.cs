using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    bool isActive = false;
    Light flashlight;
    [SerializeField] float battery;
    [SerializeField] float drainRate;
    [SerializeField] float rechargeAmount;

    void Start()
    {
        flashlight = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        if(isActive) battery -= Time.deltaTime * (drainRate);
        if(battery <= 0f) FlashOff();
        Debug.Log(battery);
    }

    public void FlashOn()
    {
        if(battery <= 0f) FlashOff();
        isActive = true;
        flashlight.enabled = !flashlight.enabled;
    }

    public void FlashOff()
    {
        flashlight.enabled = false;
        isActive = false;
    }

    public void Recharge()
    {
        battery += rechargeAmount;
    }
}
