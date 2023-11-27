using System;
using UnityEngine;

namespace CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyLevelSpawnStaticData
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyID ID { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public EnemyLevelSpawnStaticData(EnemyID id, Vector3 at)
        {
            ID = id;
            Position = at;
        }

        public void OnValidate()
        {
            _inspectorName = ID.ToString();
        }
    }
}