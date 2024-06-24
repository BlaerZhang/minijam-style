using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NightCity : MonoBehaviour
{
    public bool isLightOn = true;

    public GameObject nightCityLightOn;
    public GameObject nightCityLightOff;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        switch (isLightOn)
        {
            case true:
                nightCityLightOn.SetActive(true);
                nightCityLightOff.SetActive(false);
                break;
            case false:
                nightCityLightOn.SetActive(false);
                nightCityLightOff.SetActive(true);
                break;
        }
    }

    public void TurnOffLight()
    {
        print("turned off");
        if (isLightOn) isLightOn = false;
        DOVirtual.DelayedCall(1, () => { isLightOn = true; }).Play();
    }
}
