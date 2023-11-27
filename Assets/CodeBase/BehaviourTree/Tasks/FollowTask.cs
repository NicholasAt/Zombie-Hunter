using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine.AI;

namespace CodeBase.BehaviourTree.Tasks
{
    public class FollowTask : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly EnemyAnimation _animator;
        private readonly EnemyFindTarget _findTarget;

        public FollowTask(NavMeshAgent agent, EnemyAnimation animator, EnemyFindTarget findTarget)
        {
            _agent = agent;
            _animator = animator;
            _findTarget = findTarget;
        }

        public override bool Evaluate()
        {
            if (_findTarget.Target == null)
            {
                return false;
            }
            _animator.StopAttack();
            _animator.PlayRunning();
            _agent.destination = _findTarget.Target.position;
            return true;
        }
    }
}