using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.States
{
    public class LoadMainMenuState : IPayloadState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly ILogicFactoryService _logicFactory;
        private readonly IInputService _inputService;
        private readonly IGameObserverService _gameObserver;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IWindowService _windowService;

        public LoadMainMenuState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadCurtain loadCurtain, IUIFactory factory,
            ILogicFactoryService logicFactory,
            IInputService inputService,
            IGameObserverService gameObserver,
            IPersistentProgressService persistentProgress,
            IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadCurtain = loadCurtain;
            _uiFactory = factory;
            _logicFactory = logicFactory;
            _inputService = inputService;
            _gameObserver = gameObserver;
            _persistentProgress = persistentProgress;
            _windowService = windowService;
        }

        public void Enter(string sceneName)
        {
            _loadCurtain.Show();
            Clean();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadCurtain.Hide();
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowId.MainMenu);
            _gameStateMachine.Enter<LoopState>();
        }

        private void Clean()
        {
            _uiFactory.Clean();
            _logicFactory.Cleanup();
            _inputService.Cleanup();
            _gameObserver.Cleanup();

            if (_persistentProgress.PlayerProgress != null)
            {
                _persistentProgress.PlayerProgress.WinLoseData?.Cleanup();
                _persistentProgress.PlayerProgress.InventorySlots?.Cleanup();
            }
        }
    }
}