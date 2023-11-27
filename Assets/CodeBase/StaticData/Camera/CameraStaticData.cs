using UnityEngine;

namespace CodeBase.StaticData.Camera
{
    [CreateAssetMenu(menuName = "Static Data/Camera Static Data", order = 0)]
    public class CameraStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject CameraBrainPrefab { get; private set; }
        [field: SerializeField] public float XClampUp { get; private set; } = 90f;
        [field: SerializeField] public float XClampDown { get; private set; } = -90f;
    }
}