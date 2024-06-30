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
        Sequence lightOffSequence = DOTween.Sequence();
        lightOffSequence
            .AppendCallback(() => isLightOn = false)
            .AppendInterval(0.1f)
            .AppendCallback(() => isLightOn = true)
            .AppendInterval(0.1f)
            .AppendCallback(() => isLightOn = false)
            .AppendInterval(0.05f)
            .AppendCallback(() => isLightOn = true)
            .AppendInterval(0.1f);

        if (isLightOn) lightOffSequence.Play().OnComplete(() => isLightOn = false);
        
        Sequence lightOnSequence = DOTween.Sequence();
        lightOnSequence
            .AppendCallback(() => isLightOn = true)
            .AppendInterval(0.1f)
            .AppendCallback(() => isLightOn = false)
            .AppendInterval(0.1f)
            .AppendCallback(() => isLightOn = true)
            .AppendInterval(0.05f)
            .AppendCallback(() => isLightOn = false)
            .AppendInterval(0.1f);
        
        DOVirtual.DelayedCall(2, () => lightOnSequence.Play().OnComplete(() => isLightOn = true)).Play().SetUpdate(false);
    }
}
