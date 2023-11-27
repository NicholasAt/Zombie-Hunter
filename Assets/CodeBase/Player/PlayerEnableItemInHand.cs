using CodeBase.Data.Inventory;
using CodeBase.Data.Items;
using CodeBase.Items;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerEnableItemInHand : MonoBehaviour
    {
        private IPersistentProgressService _persistentProgress;
        private ICloseItemInHand _closeItemInHand;
        private IGameFactory _factory;
        private UnityEngine.Camera _mainCamera;

        public void Construct(IPersistentProgressService persistentProgress, IGameFactory factory, UnityEngine.Camera mainCamera)
        {
            _persistentProgress = persistentProgress;
            _factory = factory;
            _mainCamera = mainCamera;
            foreach (InventorySlot slot in _persistentProgress.PlayerProgress.InventorySlots.Slots)
            {
                slot.OnSelect += ChangeItemInHand;
            }
        }

        private void ChangeItemInHand(bool isSelect)
        {
            _closeItemInHand?.Close();
            _closeItemInHand = null;

            if (isSelect == false)
                return;

            foreach (InventorySlot slot in _persistentProgress.PlayerProgress.InventorySlots.Slots)
            {
                if (slot.IsSelect == false)
                    continue;

                BaseItemData item = slot.GetItem();
                if (item != null)
                    _closeItemInHand = _factory.CreateItemInHand(item, _mainCamera.transform);
                break;
            }
        }
    }
}