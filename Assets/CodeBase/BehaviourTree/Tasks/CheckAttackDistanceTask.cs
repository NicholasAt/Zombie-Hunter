using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class CheckAttackDistanceTask : Node
    {
        private readonly EnemyAnimation _animation;
        private readonly float _attackDistance;
        private readonly Transform _transform;
        private readonly EnemyFindTarget _findTarget;

        public CheckAttackDistanceTask(Transform transform, EnemyFindTarget findTarget, EnemyAnimation enemyAnimation, float attackDistance)
        {
            _transform = transform;
            _findTarget = findTarget;
            _animation = enemyAnimation;
            _attackDistance = attackDistance;
        }

        public override bool Evaluate()
        {
            if (_findTarget.Target == null)
            {
                _animation.StopAttack();
                return false;
            }
            if (Vector3.Distance(_transform.position, _findTarget.Target.position) <= _attackDistance)
            {
                _animation.StopRunning();
                _animation.PlayAttack();
                return true;
            }

            return false;
        }
    }
}