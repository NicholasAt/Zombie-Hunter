using CodeBase.StaticData.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level Static Data", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public Vector3 PlayerInitialPosition { get; private set; }
        public List<EnemyLevelSpawnStaticData> EnemyLevelSpawnData;

        public void SetData(Vector3 playerInitialPosition, List<EnemyLevelSpawnStaticData> enemyLevelSpawnData)
        {
            PlayerInitialPosition = playerInitialPosition;
            EnemyLevelSpawnData = enemyLevelSpawnData;
        }

        private void OnValidate()
        {
            EnemyLevelSpawnData.ForEach(x => x.OnValidate());
        }
    }
}