using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Item;
using UnityEngine;

public class Band : MonoBehaviour
{
    [Header("Band Member")]
    public List<BandMember> bandMembers = new();
    private bool _isWholePlaying = false;

    [Header("Banner Settings")]
    // show after all band members are playing
    public SpriteRenderer banner;
    public float fadeDuration = 1f;

    void Update()
    {
        CheckIfWholeBandPlaying();
    }

    private void CheckIfWholeBandPlaying()
    {
        int playingMemberCount = 0;
        foreach (var member in bandMembers)
        {
            if (member.isPlaying) playingMemberCount++;
        }

        if (playingMemberCount == bandMembers.Count)
        {
            ShowBanner();
            _isWholePlaying = true;
        }
    }

    private void ShowBanner()
    {
        if (_isWholePlaying) return;
        print("show banner");
        banner.gameObject.SetActive(true);
        banner.DOFade(1, fadeDuration);
    }
}
