using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class WinLoseData
    {
        [field: SerializeField] public int WinCount { get; private set; }
        [field: SerializeField] public int LoseCount { get; private set; }

        public Action OnWinChange;
        public Action OnLoseChange;

        public void Cleanup()
        {
            OnWinChange = null;
            OnLoseChange = null;
        }

        public void IncrementWin()
        {
            WinCount++;
            OnWinChange?.Invoke();
        }

        public void IncrementLose()
        {
            LoseCount++;
            OnLoseChange?.Invoke();
        }
    }
}