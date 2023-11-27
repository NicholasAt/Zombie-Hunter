using CodeBase.Data.Items;
using CodeBase.StaticData.Items;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext
{
    public abstract class BaseUISlotContext : MonoBehaviour
    {
        protected BaseItemConfig ItemConfig;
        protected BaseItemData ItemData;
        private Sprite _emptyIcon;
        private Image _iconImage;

        public void SetItem(Image iconImage, Sprite emptyIcon, BaseItemData itemData, BaseItemConfig itemConfig)
        {
            _iconImage = iconImage;
            ItemData = itemData;
            ItemConfig = itemConfig;
            _emptyIcon = emptyIcon;
            _iconImage.sprite = itemConfig.Icon;
            OnInitialize();
        }

        public abstract void Refresh();

        public virtual void Remove()
        {
            _iconImage.sprite = _emptyIcon;
            Destroy(gameObject);
        }

        protected virtual void OnInitialize()
        { }
    }
}