using CodeBase.Data.Items;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext
{
    public class FirearmUISlotContext : BaseUISlotContext
    {
        [SerializeField] private TMP_Text _ammotText;
        private FirearmItemData _itemData;

        public override void Refresh()
        {
            _ammotText.text = _itemData.CurrentAmmo.ToString();
        }

        protected override void OnInitialize()
        {
            _itemData = ItemData as FirearmItemData;
        }
    }
}