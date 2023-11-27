using CodeBase.UI.Windows.Inventory.RefreshInventory;
using CodeBase.UI.Windows.Inventory.RefreshInventory.SlotContext;
using UnityEngine;

namespace CodeBase.StaticData.Inventory
{
    [CreateAssetMenu(menuName = "Static Data/Inventory Static Data", order = 0)]
    public class InventoryStaticData : ScriptableObject
    {
        [field: SerializeField] public Sprite SelectSlotIcon { get; private set; }
        [field: SerializeField] public Sprite DeselectSlotIcon { get; private set; }
        [field: SerializeField] public Sprite EmptyIcon { get; private set; }
        [field: SerializeField] public int InventorySlotsCount { get; private set; } = 3;
        [field: SerializeField] public RefreshInventoryUISlot RefreshSlotPrefab { get; private set; }
        [field: SerializeField] public FirearmUISlotContext FirearmSlotPrefab { get; private set; }
    }
}