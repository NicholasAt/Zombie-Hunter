using CodeBase.Enemy;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerKillCounter : MonoBehaviour
    {
        private int _currentCounter;
        private IGameObserverService _gameObserver;
        private EnemyHealth[] _enemyHealths;
        private IPersistentProgressService _persistentProgress;
        private ISaveLoadProgressService _saveLoadProgress;

        public void Construct(IGameObserverService gameObserver, IPersistentProgressService persistentProgress, ISaveLoadProgressService saveLoadProgress, EnemyHealth[] enemyHealths)
        {
            _gameObserver = gameObserver;
            _enemyHealths = enemyHealths;
            _currentCounter = _enemyHealths.Length;
            _persistentProgress = persistentProgress;
            _saveLoadProgress = saveLoadProgress;

            foreach (EnemyHealth health in _enemyHealths)
                health.OnHappened += Count;
        }

        private void Count()
        {
            _currentCounter--;

            if (_currentCounter < 1)
            {
                _persistentProgress.PlayerProgress.WinLoseData.IncrementWin();
                _saveLoadProgress.SaveProgress();
                _gameObserver.SendPlayerWin();
            }
        }
    }
}