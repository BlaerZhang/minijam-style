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

        private int _currentLevel = 0;

        private void Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// detect if the photo meets the level objective
        /// </summary>
        /// <param name="tagsInPhoto"></param>
        public void DetectObjectives(List<string> tagsInPhoto)
        {
            LevelObjectivesSO.LevelObjective currentLevelObjective = levelObjectivesSo.LevelObjectives[_currentLevel];

            foreach (var objTag in tagsInPhoto)
            {
                if (currentLevelObjective.failingObjectiveTags.Contains(objTag))
                {
                    // TODO: show level failing panel
                    print("level fails");

                    ReloadLevel();
                    return;
                }

                if (currentLevelObjective.winningObjectiveTags.Contains(objTag))
                {
                    currentLevelObjective.winningObjectiveTags.Remove(objTag);
                }
            }

            if (currentLevelObjective.winningObjectiveTags.Count == 0)
            {
                // TODO: show the completed objective
                print("a objective complete");
            }
            else
            {
                // TODO: show level failing panel
                print("level fails");

                ReloadLevel();
            }
        }

        /// <summary>
        /// called when level fails
        /// </summary>
        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // /// <summary>
        // /// called when the photo meets all requirements
        // /// </summary>
        // private void NextLevel()
        // {
        //
        //     if (currentLevel < SceneManager.sceneCount - 1) currentLevel++;
        // }
    }
}