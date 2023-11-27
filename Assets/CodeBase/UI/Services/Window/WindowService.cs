using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using System;

namespace CodeBase.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _uiFactory.OnWindowClose += RemoveInContainer;
        }

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.None:
                    break;

                case WindowId.Inventory:
                    _uiFactory.CreateInventory();
                    break;

                case WindowId.Lose:
                    _uiFactory.CreateLose();
                    break;

                case WindowId.Win:
                    _uiFactory.CreateWin();
                    break;

                case WindowId.HUD:
                    _uiFactory.CreateHUD();
                    break;

                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        public void Close(WindowId id)
        {
            if (GetWindow(id, out BaseWindow window))
                window.Close();
        }

        public bool GetWindow<TWindow>(WindowId id, out TWindow window) where TWindow : BaseWindow
        {
            window = _uiFactory.WindowsContainer.TryGetValue(id, out var valueWindow) ? (TWindow)valueWindow : null;
            return window;
        }

        private void RemoveInContainer(WindowId id) =>
            _uiFactory.WindowsContainer.Remove(id);
    }
}