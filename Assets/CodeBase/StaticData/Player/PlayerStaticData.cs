using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public float StartHealth { get; private set; } = 20f;
        [field: SerializeField] public float JumpForce { get; private set; } = 5f;
        [field: SerializeField] public float Speed { get; private set; } = 4f;
        [field: SerializeField] public float SprintSpeed { get; private set; } = 8f;
        [field: SerializeField] public float ConstantForceY { get; private set; } = -10f;
    }
}