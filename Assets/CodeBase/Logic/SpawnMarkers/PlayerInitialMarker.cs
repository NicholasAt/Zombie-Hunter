using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers
{
    public class PlayerInitialMarker : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}