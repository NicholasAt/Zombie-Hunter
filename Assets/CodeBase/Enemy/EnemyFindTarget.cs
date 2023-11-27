using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.Observer;
using UnityEngine;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Enemy
{
    public class EnemyFindTarget : MonoBehaviour
    {
        public IApplyDamage TargetHealth { get; private set; }
        public Transform Target { get; private set; }

        private IGameObserverService _gameObserver;
        private bool _triggered;

        public void Construct(IGameObserverService gameObserver)
        {
            _gameObserver = gameObserver;
            _gameObserver.OnPlayerLose += CleanTarget;
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= CleanTarget;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;
            _triggered = true;

            if (other.CompareTag(PlayerTag))
            {
                Target = other.transform;

                if (other.attachedRigidbody.TryGetComponent(out IApplyDamage applyDamage) == false)
                    Debug.LogError("Health is missing " + other.name);
                else
                    TargetHealth = applyDamage;
            }
        }

        private void CleanTarget()
        {
            Target = null;
        }
    }
}