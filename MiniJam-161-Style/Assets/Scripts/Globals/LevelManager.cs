using System;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Globals
{
    /// <summary>
    /// receive the objects captured by the camera
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        public LevelObjectivesSO levelObjectivesSo;

        private int currentLevel = 0;

        private void Start()
        {
            throw new NotImplementedException();
        }

        // TODO: register the photo shooting function with DetectObjectives()
        private void OnEnable()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// detect if the photo meets the level objective
        /// </summary>
        /// <param name="tagsInPhoto"></param>
        public bool DetectObjectives(List<string> tagsInPhoto)
        {
            LevelObjectivesSO.LevelObjective currentLevelObjective = levelObjectivesSo.LevelObjectives[currentLevel];

            foreach (var objTag in tagsInPhoto)
            {
                if (currentLevelObjective.failingObjectiveTags.Contains(objTag))
                {
                    return false;
                }

                if (currentLevelObjective.winningObjectiveTags.Contains(objTag))
                {
                    currentLevelObjective.winningObjectiveTags.Remove(objTag);
                }
            }

            if (currentLevelObjective.winningObjectiveTags.Count == 0) return true;

            return false;
        }

        /// <summary>
        /// called when level fails
        /// </summary>
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// called when the photo meets all requirements
        /// </summary>
        public void NextLevel()
        {
            // TODO: load the next level

            if (currentLevel < SceneManager.sceneCount - 1) currentLevel++;
        }
    }
}