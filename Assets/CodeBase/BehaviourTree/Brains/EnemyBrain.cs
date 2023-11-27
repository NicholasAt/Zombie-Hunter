using CodeBase.BehaviourTree.Tasks;
using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using CodeBase.Services.Observer;
using CodeBase.StaticData.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.BehaviourTree.Brains
{
    public class EnemyBrain : TreeAI
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimation _animator;
        [SerializeField] private EnemyFindTarget _findTarget;
        private EnemyConfig _config;
        private IGameObserverService _gameObserver;

        public void Construct(EnemyConfig config, IGameObserverService gameObserver)
        {
            _config = config;
            _gameObserver = gameObserver;

            _gameObserver.OnPlayerLose += PlayerLose;
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= PlayerLose;
        }

        public override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckAttackDistanceTask(transform, _findTarget, _animator,_config.AttackDistance),
                    new AttackTask(_config.AttackDelay, _findTarget),
                }),
                new Sequence(new List<Node>
                {
                    new FollowTask(_agent, _animator, _findTarget),
                }),
            });
            return root;
        }

        private void PlayerLose()
        {
            _agent.enabled = false;
        }
    }
}