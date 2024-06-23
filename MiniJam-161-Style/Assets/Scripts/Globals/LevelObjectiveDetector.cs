using System;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

namespace Globals
{
    /// <summary>
    /// receive the objects captured by the camera
    /// </summary>
    public class LevelObjectiveDetector : MonoBehaviour
    {
        public LevelObjectivesSO LevelObjectivesSO;

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
            LevelObjectivesSO.LevelObjective currentLevelObjective = LevelObjectivesSO.LevelObjectives[GameManager.Instance.currentLevel];

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
    }
}