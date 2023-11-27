using System;

namespace CodeBase.Services.Observer
{
    public interface IGameObserverService : IService
    {
        void Cleanup();

        Action OnPlayerLose { get; set; }
        Action OnPlayerWin { get; set; }

        void SendPlayerLose();

        void SendPlayerWin();
    }
}