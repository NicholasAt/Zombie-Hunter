using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Inventory;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.RefreshInventory
{
    public class RefreshInventoryUISlot : MonoBehaviour
    {
        [SerializeField] private Transform _contextRoot;
        [SerializeField] private Image _backgorundImage;
        [SerializeField] private Image _iconImage;

        private InventorySlot _slot;
        private IUIFactory _uiFactory;

        private BaseUISlotContext _slotContext;
        private InventoryStaticData _config;

        public void Construct(InventorySlot slot, IUIFactory uiFactory, IStaticDataService dataService)
        {
            _slot = slot;
            _uiFactory = uiFactory;
            _config = dataService.InventoryStaticData;

            _slot.OnSetItem += SetItem;
            _slot.OnCleanItem += CleanItem;
            _slot.OnSelect += ChangeSelectState;
        }

        public void Initialize()
        {
            SetItem();
            ChangeSelectState(_slot.IsSelect);
        }

        private void OnDestroy()
        {
            CleanItem();
        }

        private void SetItem()
        {
            BaseItemData itemData = _slot.GetItem();
            if (itemData == null)
                return;
            if (_slotContext != null)
            {
                _slot.OnItemValueChange -= _slotContext.Refresh;
                _slotContext.Remove();
                _slotContext = null;
            }

            _slotContext = _uiFactory.CreateInventorySlotContext(_contextRoot, itemData, _iconImage);
            _slot.OnItemValueChange += _slotContext.Refresh;
            _slotContext.Refresh();
        }

        private void CleanItem()
        {
            _slot.OnSelect -= ChangeSelectState;

            if (_slotContext != null)
            {
                _slot.OnItemValueChange -= _slotContext.Refresh;
                _slotContext.Remove();
                _slotContext = null;
            }
        }

        private void ChangeSelectState(bool value) =>
            _backgorundImage.sprite = value ? _config.SelectSlotIcon : _config.DeselectSlotIcon;
    }
}