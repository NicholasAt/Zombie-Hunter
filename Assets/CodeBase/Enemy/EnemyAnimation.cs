using CodeBase.Services.Observer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        private readonly int RunningHash = Animator.StringToHash("Running");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int DieHash = Animator.StringToHash("Die");

        [SerializeField] private Animator _animator;
        private IGameObserverService _gameObserver;

        public void Construct(IGameObserverService gameObserver)
        {
            _gameObserver = gameObserver;
            _gameObserver.OnPlayerLose += PlayerLose;
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= PlayerLose;
        }

        public void PlayRunning()
        {
            _animator.SetBool(RunningHash, true);
        }

        public void StopRunning()
        {
            _animator.SetBool(RunningHash, false);
        }

        public void PlayAttack()
        {
            _animator.SetBool(AttackHash, true);
        }

        public void StopAttack()
        {
            _animator.SetBool(AttackHash, false);
        }

        public void PlayDead()
        {
            StopAttack();
            _animator.applyRootMotion = true;
            _animator.Play(DieHash, 0, 0);
        }

        private void PlayerLose()
        {
            StopRunning();
            StopAttack();
        }
    }
}