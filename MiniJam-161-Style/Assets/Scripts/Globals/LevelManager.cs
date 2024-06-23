using System;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Globals
{
    /// <summary>
    /// receive the objects captured by the camera
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelObjectivesSO levelObjectivesSO;

        [HideInInspector] public LevelObjectivesSO.LevelObjective currentLevelObjective;

        // level
        private int _maxLevel;
        private int currentLevelIndex = 0;

        private void Start()
        {
            _maxLevel = levelObjectivesSO.levelObjectives.Count;
            StartNewLevel();
        }

        private void StartNewLevel()
        {
            currentLevelObjective = levelObjectivesSO.levelObjectives[currentLevelIndex];
        }

        public void NextLevel()
        {
            if (currentLevelIndex < _maxLevel - 1)
            {
                currentLevelIndex++;
                StartNewLevel();
            }
            else
            {
                // TODO: finish the game
            }
        }
    }
}