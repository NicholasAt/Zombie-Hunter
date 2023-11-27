using CodeBase.Data;
using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items;
using UnityEngine;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadProgressService : ISaveLoadProgressService
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _dataService;

        public SaveLoadProgressService(IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _persistentProgressService = persistentProgressService;
            _dataService = dataService;
        }

        public void SaveProgress()
        {
            string json = JsonUtility.ToJson(_persistentProgressService.PlayerProgress.WinLoseData);
            PlayerPrefs.SetString(SaveProgressPrefsKey, json);
        }

        public void LoadProgress()
        {
            PlayerProgress newProgress = new PlayerProgress();
            newProgress.WinLoseData = LoadOrInitNew();

            newProgress.InventorySlots = new InventorySlotsContainer();
            newProgress.InventorySlots.Slots = new InventorySlot[_dataService.InventoryStaticData.InventorySlotsCount];

            for (var i = 0; i < newProgress.InventorySlots.Slots.Length; i++)
            {
                InventorySlot slot = new InventorySlot();
                newProgress.InventorySlots.Slots[i] = slot;
            }
            newProgress.InventorySlots.Slots[0].SetItem(new FirearmItemData(55, ItemID.Guns_Pistol));//start gun
            newProgress.InventorySlots.Slots[1].SetItem(new FirearmItemData(555, ItemID.Guns_M4));//start gun
            _persistentProgressService.PlayerProgress = newProgress;
        }

        private static WinLoseData LoadOrInitNew()
        {
            string prefsProgress = PlayerPrefs.GetString(SaveProgressPrefsKey);
            WinLoseData data = JsonUtility.FromJson<WinLoseData>(prefsProgress);
            return data ?? new WinLoseData();
        }
    }
}