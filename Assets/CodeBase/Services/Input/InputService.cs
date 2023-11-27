using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        #region Player

        public Vector2 MoveAxis => _mainControls.Player.Move.ReadValue<Vector2>();
        public Vector2 CameraAxis => _mainControls.Player.Camera.ReadValue<Vector2>();
        public bool IsSprint => _mainControls.Player.Sprint.IsPressed();
        public Action OnJump { get; set; }

        #endregion Player

        #region UseItems

        public bool UseItem1 => _mainControls.UseItems.UseItem1.IsPressed();
        public bool UseItem2 => _mainControls.UseItems.UseItem2.IsPressed();

        #endregion UseItems

        #region Inventory

        public Action<int> OnChangeSlot { get; set; }

        #endregion Inventory

        private readonly MainControls _mainControls;

        public InputService()
        {
            _mainControls = new MainControls();
            EnableActions();

            InitPlayer();
            InitInventory();
        }

        public void Cleanup()
        {
            OnJump = null;
            OnChangeSlot = null;
        }

        private void EnableActions()
        {
            _mainControls.Player.Enable();
            _mainControls.Inventory.Enable();
            _mainControls.UseItems.Enable();
        }

        private void InitInventory()
        {
            _mainControls.Inventory.ChangeSlot.performed += index => OnChangeSlot?.Invoke((int)index.ReadValue<float>());
        }

        private void InitPlayer()
        {
            _mainControls.Player.Jump.performed += _ => OnJump?.Invoke();
        }
    }
}