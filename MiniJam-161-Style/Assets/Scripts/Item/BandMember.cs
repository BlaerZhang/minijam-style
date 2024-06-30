using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class BandMember : MonoBehaviour
    {
        public Sprite playingSprite;
        public GameObject playingVfx;

        private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
        }

        public void StartPlaying()
        {
            _spriteRenderer.sprite = playingSprite;
            playingVfx.SetActive(true);
        }

        public void StopPlaying()
        {
            _spriteRenderer.sprite = _originalSprite;
            playingVfx.SetActive(false);
        }
    }
}