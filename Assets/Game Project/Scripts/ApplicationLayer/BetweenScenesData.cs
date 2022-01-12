using System;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer
{
    public sealed class BetweenScenesData: MonoBehaviour
    {
        public int levelIndex;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void SetLevelIndex(int levelIndex)
        {
            this.levelIndex = levelIndex;
        }
    }
}