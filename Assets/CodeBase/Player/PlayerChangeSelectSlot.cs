using CodeBase.Logic;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Observer;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerChangeSelectSlot : MonoBehaviour
    {
        private InventorySlotsHandler _slotsHandler;
        private IInputService _inputService;
        private IGameObserverService _gameObserver;

        public void Construct(ILogicFactoryService logicFactory, IInputService inputService, IGameObserverService gameObserver)
        {
            _slotsHandler = logicFactory.InventorySlotsHandler;
            _inputService = inputService;
            _gameObserver = gameObserver;

            _inputService.OnChangeSlot += ChangeSlot;
            _gameObserver.OnPlayerLose += Lose;
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= Lose;
            _inputService.OnChangeSlot -= ChangeSlot;
        }

        private void ChangeSlot(int index)
        {
            _slotsHandler.SetSelectSlot(index);
        }

        private void Lose()
        {
            _inputService.OnChangeSlot -= ChangeSlot;
            enabled = false;
        }
    }
}