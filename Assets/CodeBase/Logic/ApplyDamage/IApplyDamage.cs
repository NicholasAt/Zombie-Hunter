namespace CodeBase.Logic.ApplyDamage
{
    public interface IApplyDamage
    {
        void ApplyDamage(float value);

        SurfaceType SurfaceType { get; }
    }
}