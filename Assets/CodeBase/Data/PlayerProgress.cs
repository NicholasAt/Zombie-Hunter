using CodeBase.Data.Inventory;
using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public InventorySlotsContainer InventorySlots;
        public WinLoseData WinLoseData;
    }
}