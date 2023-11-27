using CodeBase.Data.Items;
using CodeBase.StaticData.Items;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace CodeBase.Data.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        [HideInInspector] [SerializeField] private FirearmItemData _firearmItemData;
        public bool IsSelect { get; private set; }
        public Action OnSetItem;
        public Action OnItemValueChange;
        public Action OnCleanItem;
        public Action<bool> OnSelect;

        public void Cleanup()
        {
            OnSetItem = null;
            OnItemValueChange = null;
            OnCleanItem = null;
            OnSelect = null;
        }

        public void SetItem(BaseItemData itemData)
        {
            switch (itemData)
            {
                case FirearmItemData firearm:
                    _firearmItemData = firearm;
                    break;

                default:
                    Debug.LogError($"I can't store the {itemData} item");
                    return;
            }
            itemData.OnValueChange += () => OnItemValueChange?.Invoke();
        }

        [CanBeNull]
        public BaseItemData GetItem()
        {
            if (_firearmItemData != null && _firearmItemData.ID != ItemID.None)
                return _firearmItemData;

            return null;
        }

        public void SetSelect(bool isSelect)
        {
            IsSelect = isSelect;
            OnSelect?.Invoke(isSelect);
        }
    }
}