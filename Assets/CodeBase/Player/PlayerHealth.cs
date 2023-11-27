using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour, IApplyDamage
    {
        public SurfaceType SurfaceType => SurfaceType.Living;

        private float _currentHealth;
        private bool _happened;
        private IGameObserverService _gameObserver;
        private IPersistentProgressService _persistentProgress;
        private ISaveLoadProgressService _saveLoadProgress;

        public void Construct(IGameObserverService gameObserver, IPersistentProgressService persistentProgress, ISaveLoadProgressService saveLoadProgress, IStaticDataService dataService)
        {
            _gameObserver = gameObserver;
            _persistentProgress = persistentProgress;
            _saveLoadProgress = saveLoadProgress;
            _currentHealth = dataService.PlayerStaticData.StartHealth;
        }

        public void ApplyDamage(float value)
        {
            if (_happened)
                return;

            _currentHealth -= value;
            Debug.Log("Health Player " + _currentHealth);
            if (_currentHealth <= 0)
            {
                _happened = true;
                _persistentProgress.PlayerProgress.WinLoseData.IncrementLose();
                _saveLoadProgress.SaveProgress();
                _gameObserver.SendPlayerLose();
            }
        }
    }
}