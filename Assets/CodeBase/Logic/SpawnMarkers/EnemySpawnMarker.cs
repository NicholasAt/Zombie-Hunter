using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public EnemyID ID { get; private set; }

        private void OnValidate()
        {
            if (gameObject.scene.rootCount != 0)
                gameObject.name = $"{ID} Spawn Marker";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}