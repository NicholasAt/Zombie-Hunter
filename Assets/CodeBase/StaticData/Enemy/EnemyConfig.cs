using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyID ID { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; } = 100f;
        [field: SerializeField] public float AttackDelay { get; private set; } = 1f;
        [field: SerializeField] public float AttackDistance { get; private set; } = 2f;

        public void OnValidate()
        {
            _inspectorName = ID.ToString();
        }
    }
}