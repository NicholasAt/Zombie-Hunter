using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 CameraAxis { get; }
        bool UseItem2 { get; }
        Action OnJump { get; set; }
        bool IsSprint { get; }
        Action<int> OnChangeSlot { get; set; }
        bool UseItem1 { get; }

        void Cleanup();
    }
}