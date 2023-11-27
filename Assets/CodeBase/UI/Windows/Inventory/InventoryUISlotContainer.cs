using UnityEngine;

namespace CodeBase.UI.Windows.Inventory
{
    public class InventoryUISlotContainer : BaseWindow
    {
        [field: SerializeField] public Transform SlotsRoot { get; private set; }
    }
}