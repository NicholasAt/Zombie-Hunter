using CodeBase.Logic.ApplyDamage;
using CodeBase.StaticData.Enemy;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IApplyDamage
    {
        public SurfaceType SurfaceType => SurfaceType.Living;
        [SerializeField] private NavMeshAgent _turnOffAgent;
        [SerializeField] private MonoBehaviour[] _turnOffBehaviours;
        [SerializeField] private Collider[] _turnOffColliders;

        [SerializeField] private EnemyAnimation _animation;

        private float _currentHealth;
        public bool Happened { get; private set; }
        public Action OnHappened;

        public void Construct(EnemyConfig config)
        {
            _currentHealth = config.Health;
        }

        public void ApplyDamage(float value)
        {
            if (Happened)
                return;

            _currentHealth -= value;
            if (_currentHealth <= 0)
            {
                Happened = true;
                foreach (MonoBehaviour behaviour in _turnOffBehaviours) behaviour.enabled = false;
                foreach (Collider collision in _turnOffColliders) collision.enabled = false;
                _turnOffAgent.enabled = false;

                _animation.PlayDead();
                OnHappened?.Invoke();
            }
        }
    }
}