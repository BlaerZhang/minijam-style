using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "LevelObjectivesSO", menuName = "ScriptableObjects/LevelObjectivesSO", order = 0)]
    public class LevelObjectivesSO : ScriptableObject
    {
        [Serializable]
        public struct LevelObjective
        {
            public string levelName;
            public string description;
            public List<string> winningObjectiveTags;
            public List<string> failingObjectiveTags;
        }

        public List<LevelObjective> levelObjectives;
    }
}