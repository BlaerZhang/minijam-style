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

        private bool _isScoping = false;
        public bool IsScoping
        {
            get => _isScoping;
            set => _isScoping = value;
        }

        private void Update()
        {
            if (_isScoping)
            {
                ReduceBatteryPercentage();
            }
            else
            {
                RecoverBatteryPercentage();
            }
        }

        private void RecoverBatteryPercentage()
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
        // TODO: bullet time count down
        private void ReduceBatteryPercentage()
        {
            if (CurrentBatteryPercentage > 0)
            {
                CurrentBatteryPercentage -= Time.deltaTime * batteryUsageSpeed;
            }
            else
            {
                CurrentBatteryPercentage = 0;
                OnBatteryDead();
            }
        }

        private void OnBatteryDead()
        {
            // TODO: quit scoping
            IsScoping = false;
        }
    }
}