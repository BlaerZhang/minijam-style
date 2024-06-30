using UnityEngine;

namespace Item
{
    public class SpriteBounceRotation : MonoBehaviour
    {
        // Rotation duration in seconds
        public float rotationDuration = 1.0f;

        private float elapsedTime = 0.0f;
        private bool rotatingForward = true;

        void Update()
        {
            // Calculate the percentage of time passed for the current rotation direction
            float t = (elapsedTime % (rotationDuration / 2.0f)) / (rotationDuration / 2.0f);

            // Determine the target angle based on the direction
            float targetAngle = rotatingForward ? Mathf.Lerp(0, 180, t) : Mathf.Lerp(180, 0, t);

            // Apply the rotation to the sprite
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Switch direction if necessary
            if (elapsedTime >= rotationDuration / 2.0f)
            {
                rotatingForward = !rotatingForward;
                elapsedTime = 0.0f;
            }
        }
    }
}