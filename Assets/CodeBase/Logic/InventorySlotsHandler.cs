using CodeBase.Data.Inventory;
using CodeBase.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class InventorySlotsHandler
    {
        public Action<InventorySlot> OnSlotChanged;

        private readonly InventorySlotsContainer _inventorySlots;
        private InventorySlot _selectedSlot;

        public InventorySlotsHandler(IPersistentProgressService persistentProgressService)
        {
            _inventorySlots = persistentProgressService.PlayerProgress.InventorySlots;
        }

        public void SetSelectSlot(int index)
        {
            if (index > _inventorySlots.Slots.Length)
            {
                Debug.LogError("it seems there are fewer slots than you expected");
                return;
            }

            InventorySlot newSlot = _inventorySlots.Slots[index];
            if (_selectedSlot == newSlot)
            {
                _selectedSlot.SetSelect(!_selectedSlot.IsSelect);
                OnSlotChanged?.Invoke(_selectedSlot);
            }
            else
            {
                _selectedSlot?.SetSelect(false);
                OnSlotChanged?.Invoke(_selectedSlot);

                _selectedSlot = newSlot;
                _selectedSlot.SetSelect(true);
                OnSlotChanged?.Invoke(_selectedSlot);
            }
        }

        public void Clean()
        {
            OnSlotChanged = null;
        }
    }
}