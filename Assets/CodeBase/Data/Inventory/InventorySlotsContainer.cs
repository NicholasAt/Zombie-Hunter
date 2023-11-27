﻿using System;

namespace CodeBase.Data.Inventory
{
    [Serializable]
    public class InventorySlotsContainer
    {
        public InventorySlot[] Slots;

        public void Cleanup()
        {
            foreach (InventorySlot slot in Slots)
                slot.Cleanup();
        }
    }
}