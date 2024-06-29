using UnityEngine;

namespace Item
{
    public class Clock : MonoBehaviour
    {
        public float rotationDuration = 5f;
        private float rotationSpeed;

        void Start()
        {
            rotationSpeed = 360f / rotationDuration;
        }

        void Update()
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}