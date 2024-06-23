using System;
using System.Collections.Generic;
using Globals;
using UnityEngine;

namespace Camera_Controller
{
    public class CameraBattery : MonoBehaviour
    {
        public float maxBatteryPercentage = 1;
        public float batteryRecoverySpeed = 1;
        public float batteryUsageSpeed = .5f;

        private float _currentBatteryPercentage;
        public float CurrentBatteryPercentage
        {
            get => _currentBatteryPercentage;
            set
            {
                _currentBatteryPercentage = value;
                GameManager.Instance.uiManager.UpdateCameraBatteryPercentage(value);
            }
        }

        public void RecoverBatteryPercentage()
        {
            if (CurrentBatteryPercentage <= 1)
            {
                CurrentBatteryPercentage += Time.deltaTime * batteryRecoverySpeed;
            }
            else
            {
                CurrentBatteryPercentage = maxBatteryPercentage;
            }
        }

        /// <summary>
        /// called when fully open the scope
        /// </summary>
        public void ReduceBatteryPercentage()
        {
            if (CurrentBatteryPercentage > 0)
            {
                CurrentBatteryPercentage -= Time.deltaTime * batteryUsageSpeed;
            }
            else
            {
                CurrentBatteryPercentage = 0;
                CameraController.OnBatteryDead?.Invoke();
            }
        }
    }
}