using UnityEngine;

namespace CodeBase.Logic.ApplyDamage
{
    public class HealthInanimate : MonoBehaviour, IApplyDamage
    {
        [field: SerializeField] public SurfaceType SurfaceType { get; private set; }

        public void SetSurface(SurfaceType surfaceType) =>
        SurfaceType = surfaceType;

        public void ApplyDamage(float value)
        { }
    }
}