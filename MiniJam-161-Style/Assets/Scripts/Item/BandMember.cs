using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    public class BandMember : MonoBehaviour, IItemAuto
    {
        public Sprite playingSprite;

        [HideInInspector] public bool isPlaying = false;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact(GameObject interactorGameObject)
        {
            print("touched member");
            _spriteRenderer.sprite = playingSprite;
            isPlaying = true;
        }
    }
}