using CodeBase.Data.Items;
using CodeBase.Enemy;
using CodeBase.Items;
using CodeBase.Logic.ApplyDamage;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 at, EnemyHealth[] enemyHealths);

        ICloseItemInHand CreateItemInHand(BaseItemData item, Transform root);

        void SetCamera(UnityEngine.Camera mainCamera);

        GameObject CreateEnemy(EnemyID id, Vector3 at);

        void CreateFirearmFXAttack(SurfaceType surfaceType, Vector3 at, Vector3 normal);
    }
}