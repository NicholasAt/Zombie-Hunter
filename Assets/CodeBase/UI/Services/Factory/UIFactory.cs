using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.Weapon.Firearms;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.HUD;
using CodeBase.UI.Windows.Inventory;
using CodeBase.UI.Windows.Inventory.RefreshInventory;
using CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext;
using CodeBase.UI.Windows.Lose;
using CodeBase.UI.Windows.MainMenu;
using CodeBase.UI.Windows.Win;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        public Dictionary<WindowId, BaseWindow> WindowsContainer { get; } = new Dictionary<WindowId, BaseWindow>();
        public Action<WindowId> OnWindowClose { get; set; }

        private Transform _uiRoot;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistentProgress;

        public UIFactory(IGameStateMachine gameStateMachine, IStaticDataService staticDataService, IPersistentProgressService persistentProgress)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _persistentProgress = persistentProgress;
        }

        public void Clean()
        {
            WindowsContainer.Clear();
            OnWindowClose = null;
        }

        public void CreateUIRoot() =>
            _uiRoot = Object.Instantiate(_staticDataService.WindowStaticData.UIRoot).transform;

        public void CreateMainMenu()
        {
            var mainMenuInstance = InstantiateRegister<MenuMenu>(WindowId.MainMenu);
            mainMenuInstance.Construct(_gameStateMachine);
        }

        public void CreateInventory()
        {
            InventoryUISlotContainer slotsContainer = InstantiateRegister<InventoryUISlotContainer>(WindowId.Inventory);
            CreateInventorySlots(slotsContainer.SlotsRoot);
        }

        public void CreateLose()
        {
            InstantiateRegister<LoseWindow>(WindowId.Lose);
        }

        public void CreateHUD()
        {
            HUDWindow hudWindow = InstantiateRegister<HUDWindow>(WindowId.HUD);
            hudWindow.GetComponentInChildren<WinLoseCounterWindow>().Construct(_persistentProgress);
        }

        public void CreateWin()
        {
            InstantiateRegister<WinWindow>(WindowId.Win);
        }

        public BaseUISlotContext CreateInventorySlotContext(Transform parent, BaseItemData itemData, Image iconImage)
        {
            switch (itemData)
            {
                case FirearmItemData:
                    FirearmUISlotContext prefab = _staticDataService.InventoryStaticData.FirearmSlotPrefab;
                    FirearmItemConfig config = _staticDataService.ForFirearmsItems(itemData.ID);

                    FirearmUISlotContext slotInstance = Object.Instantiate(prefab, parent);
                    slotInstance.SetItem(iconImage, _staticDataService.InventoryStaticData.EmptyIcon, itemData, config);
                    return slotInstance;
            }

            Debug.LogError($"I couldn't create a slot with the {itemData} item");
            return null;
        }

        private void CreateInventorySlots(Transform parent)
        {
            foreach (InventorySlot inventorySlot in _persistentProgress.PlayerProgress.InventorySlots.Slots)
            {
                RefreshInventoryUISlot refreshSlotInstance = Object.Instantiate(_staticDataService.InventoryStaticData.RefreshSlotPrefab, parent);
                refreshSlotInstance.Construct(inventorySlot, this, _staticDataService);
                refreshSlotInstance.Initialize();
            }
        }

        private TWindow InstantiateRegister<TWindow>(WindowId id) where TWindow : BaseWindow
        {
            WindowConfig config = _staticDataService.ForWindow(id);
            BaseWindow window = Object.Instantiate(config.Template, _uiRoot);

            window.SetId(id);
            window.OnClosed += SendOnClosed;
            WindowsContainer[id] = window;
            return (TWindow)window;
        }

        private void SendOnClosed(WindowId id) =>
           OnWindowClose?.Invoke(id);
    }
}