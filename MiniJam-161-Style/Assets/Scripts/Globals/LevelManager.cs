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
        public LevelObjectivesSO levelObjectivesSO;

        // level
        private int _maxLevel;
        public int currentLevel = 0;

        private void Start()
        {
            _maxLevel = levelObjectivesSO.levelObjectives.Count;
        }

        /// <summary>
        /// detect if the photo meets the level objective
        /// </summary>
        /// TODO: fix
        public void DetectObjectives(List<string> tagsFullyInPhoto, List<string> tagsPartlyInPhoto)
        {
            LevelObjectivesSO.LevelObjective currentLevelObjective = levelObjectivesSO.levelObjectives[currentLevel];

            foreach (var objTag in tagsPartlyInPhoto)
            {
                if (currentLevelObjective.failingObjectiveTags.Contains(objTag))
                {
                    // TODO: show wrong target panel
                    print("wrong target in photo");
                }
            }

            foreach (var objTag in tagsFullyInPhoto)
            {
                if (currentLevelObjective.winningObjectiveTags.Contains(objTag))
                {
                    currentLevelObjective.winningObjectiveTags.Remove(objTag);
                }
            }

            if (currentLevelObjective.winningObjectiveTags.Count == 0)
            {
                // TODO: show the completed objective
                print("an objective completed");

                if (currentLevel < _maxLevel - 1) currentLevel++;
            }
            else
            {
                // TODO: show not enough target panel
                print("not all targets in photo");
            }
        }

        /// <summary>
        /// called when level fails
        /// </summary>
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}