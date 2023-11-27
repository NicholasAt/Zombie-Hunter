using CodeBase.Logic.SpawnMarkers;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class LevelStaticDataEditor
    {
        private const string LevelStaticDataPath = "StaticData/Level/LevelStaticData";

        [MenuItem("Tools/Collect Level Data")]
        private static void Collect()
        {
            LevelStaticData data = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            List<EnemyLevelSpawnStaticData> enemySpawnData = Object.FindObjectsOfType<EnemySpawnMarker>().Select(x => new EnemyLevelSpawnStaticData(x.ID, x.transform.position)).ToList();

            data.SetData(Object.FindObjectOfType<PlayerInitialMarker>().transform.position, enemySpawnData);

            EditorUtility.SetDirty(data);
        }
    }
}