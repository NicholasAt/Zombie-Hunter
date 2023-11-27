using CodeBase.Data.Items;
using CodeBase.Services;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();

        Dictionary<WindowId, BaseWindow> WindowsContainer { get; }
        Action<WindowId> OnWindowClose { get; set; }

        void Clean();

        void CreateInventory();

        BaseUISlotContext CreateInventorySlotContext(Transform parent, BaseItemData itemData, Image iconImage);

        void CreateLose();

        void CreateWin();

        void CreateHUD();

        void CreateMainMenu();
    }
}