using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Item;
using UnityEngine;

public class Band : MonoBehaviour
{
    [Header("Trigger Settings")]
    public int humanoidInRangeTriggerCount = 3;
    public List<string> humanoidTags;

    [Header("Band Member")]
    public List<BandMember> bandMembers = new();

    [Header("Banner Settings")]
    // show after all band members are playing
    public SpriteRenderer banner;
    public float fadeDuration = 2f;

    private int _currentHumanoidInRange = 0;
    private bool _hasBannerShown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentHumanoidInRange == humanoidInRangeTriggerCount) return;
        
        if (humanoidTags.Contains(other.tag))
        {
            bandMembers[_currentHumanoidInRange].StartPlaying();
            _currentHumanoidInRange++;

            if (_currentHumanoidInRange == humanoidInRangeTriggerCount)
            {
                ShowBanner();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentHumanoidInRange == 0) return;

        if (humanoidTags.Contains(other.tag))
        {
            _currentHumanoidInRange--;
            bandMembers[_currentHumanoidInRange].StopPlaying();

            if (_currentHumanoidInRange == humanoidInRangeTriggerCount - 1)
            {
                HideBanner();
            }
        }
    }

    private void ShowBanner()
    {
        banner.gameObject.SetActive(true);
        banner.DOFade(1, fadeDuration);
    }

    private void HideBanner()
    {
        banner.DOFade(0, fadeDuration / 4)
            .OnComplete(() =>
            {
                banner.gameObject.SetActive(false);
            });
    }
}
