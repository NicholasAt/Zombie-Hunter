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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowStaticDataPath = "StaticData/UI/Windows/WindowStaticData";
        private const string PlayerStaticDataPath = "StaticData/Player/PlayerStaticData";
        private const string CameraStaticDataPath = "StaticData/Camera/CameraStaticData";
        private const string LevelStaticDataPath = "StaticData/Level/LevelStaticData";
        private const string EnemyStaticDataPath = "StaticData/Enemy/EnemyStaticData";
        private const string InventoryStaticDataPath = "StaticData/Inventory/InventoryStaticData";
        private const string FirearmsStaticDataPath = "StaticData/Items/Weapon/Firearms/FirearmsStaticData";

        public PlayerStaticData PlayerStaticData { get; private set; }
        public WindowStaticData WindowStaticData { get; private set; }
        public CameraStaticData CameraStaticData { get; private set; }
        public LevelStaticData LevelStaticData { get; private set; }
        public InventoryStaticData InventoryStaticData { get; private set; }
        public FirearmsStaticData FirearmsStaticData { get; private set; }

        private Dictionary<EnemyID, EnemyConfig> _enemyConfigs;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<ItemID, FirearmItemConfig> _firearmItemConfigs;
        private Dictionary<SurfaceType, AttackFXConfig> _firearmFXConfigs;

        public void Load()
        {
            LoadWindows();
            PlayerStaticData = Resources.Load<PlayerStaticData>(PlayerStaticDataPath);
            CameraStaticData = Resources.Load<CameraStaticData>(CameraStaticDataPath);
            LevelStaticData = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            InventoryStaticData = Resources.Load<InventoryStaticData>(InventoryStaticDataPath);
            _enemyConfigs = Resources.Load<EnemyStaticData>(EnemyStaticDataPath).Configs.ToDictionary(x => x.ID, x => x);

            LoadItems();
        }

        public FirearmItemConfig ForFirearmsItems(ItemID id) =>
            _firearmItemConfigs.TryGetValue(id, out FirearmItemConfig config) ? config : null;

        public AttackFXConfig ForFirearmFX(SurfaceType surfaceType) =>
            _firearmFXConfigs.TryGetValue(surfaceType, out AttackFXConfig cfg) ? cfg : null;

        public EnemyConfig ForEnemy(EnemyID id) =>
            _enemyConfigs.TryGetValue(id, out EnemyConfig cfg) ? cfg : null;

        public WindowConfig ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out WindowConfig data) ? data : null;

        private void LoadWindows()
        {
            WindowStaticData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
            _windowConfigs = WindowStaticData.Configs.ToDictionary(x => x.WindowId, x => x);
        }

        private void LoadItems()
        {
            Firearms();

            void Firearms()
            {
                FirearmsStaticData = Resources.Load<FirearmsStaticData>(FirearmsStaticDataPath);
                _firearmItemConfigs = new Dictionary<ItemID, FirearmItemConfig>(FirearmsStaticData.Configs.Capacity);
                _firearmFXConfigs = FirearmsStaticData.FXStaticData.FXConfigs.ToDictionary(x => x.SurfaceType, x => x);

                foreach (FirearmItemConfig config in FirearmsStaticData.Configs)
                {
                    _firearmItemConfigs.Add(config.ID, config);
                }
            }
        }
    }
}