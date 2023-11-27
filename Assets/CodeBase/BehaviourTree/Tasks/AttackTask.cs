using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class AttackTask : Node
    {
        private readonly float _attackDelay;
        private readonly EnemyFindTarget _findTarget;

        private float _currentDelay;

        public AttackTask(float attackDelay, EnemyFindTarget findTarget)
        {
            _attackDelay = attackDelay;
            _findTarget = findTarget;
        }

        public override bool Evaluate()
        {
            if (_findTarget.Target == null)
            {
                return false;
            }
            _currentDelay += Time.deltaTime;
            if (_currentDelay >= _attackDelay)
            {
                _findTarget.TargetHealth.ApplyDamage(5);
                _currentDelay = 0;
            }
            return true;
        }
    }
}