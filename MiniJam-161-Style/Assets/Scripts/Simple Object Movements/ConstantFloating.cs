using System;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace Simple_Object_Movements
{
    public class ConstantFloating : MonoBehaviour
    {
        public float floatDistance = 0.1f;
        public float floatDuration = 1f;

        private Tween floatTween;

        private void Awake()
        {
            Vector3 initialPosition = transform.position;

            floatTween = transform.DOMoveY(initialPosition.y + floatDistance, floatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .OnUpdate(() =>
                {
                    if (transform.position.y > initialPosition.y + floatDistance)
                    {
                        transform.position = new Vector3(transform.position.x, initialPosition.y + floatDistance, transform.position.z);
                    }
                    else if (transform.position.y < initialPosition.y)
                    {
                        transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
                    }
                });

            floatTween.Pause();
        }

        private void OnEnable()
        {
            print("play");
            floatTween.Play();
        }

        private void OnDisable()
        {
            print("pause");
            floatTween.Pause();
        }
    }
}