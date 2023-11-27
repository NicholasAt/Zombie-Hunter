using CodeBase.Logic.ApplyDamage;
using CodeBase.StaticData.AttackFX;
using CodeBase.StaticData.Camera;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Inventory;
using CodeBase.StaticData.Items;
using CodeBase.StaticData.Items.Weapon.Firearms;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Window;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        WindowConfig ForWindow(WindowId id);

        WindowStaticData WindowStaticData { get; }
        PlayerStaticData PlayerStaticData { get; }
        CameraStaticData CameraStaticData { get; }
        LevelStaticData LevelStaticData { get; }
        InventoryStaticData InventoryStaticData { get; }
        FirearmsStaticData FirearmsStaticData { get; }

        FirearmItemConfig ForFirearmsItems(ItemID id);

        EnemyConfig ForEnemy(EnemyID id);

        AttackFXConfig ForFirearmFX(SurfaceType surfaceType);
    }
}