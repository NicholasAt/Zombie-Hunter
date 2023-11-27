using CodeBase.UI.Services.Window;
using System;

namespace CodeBase.Services.Observer
{
    public class GameObserverService : IGameObserverService
    {
        private readonly IWindowService _windowService;

        public Action OnPlayerLose { get; set; }
        public Action OnPlayerWin { get; set; }

        public GameObserverService(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void SendPlayerLose()
        {
            OnPlayerLose?.Invoke();
            _windowService.Open(WindowId.Lose);
        }

        public void SendPlayerWin()
        {
            OnPlayerWin?.Invoke();
            _windowService.Open(WindowId.Win);
        }

        public void Cleanup()
        {
            OnPlayerWin = null;
            OnPlayerLose = null;
        }
    }
}