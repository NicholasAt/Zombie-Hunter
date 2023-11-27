using CodeBase.Infrastructure.Logic;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using static CodeBase.Data.GameConstants;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            RegisterStaticData();
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IInputService>(new InputService());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_stateMachine, _services.Single<IStaticDataService>(), _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IGameObserverService>(new GameObserverService(_services.Single<IWindowService>()));

            _services.RegisterSingle<ISaveLoadProgressService>(new SaveLoadProgressService(
                _services.Single<IPersistentProgressService>(),
                _services.Single<IStaticDataService>()));

            _services.RegisterSingle<ILogicFactoryService>(new LogicFactoryService(_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IInputService>(),
                _services.Single<IStaticDataService>(),
                _services.Single<ILogicFactoryService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IGameObserverService>(),
                _services.Single<ISaveLoadProgressService>()));
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadMainMenuState, string>(MainMenuSceneKey);
        }

        private void RegisterStaticData()
        {
            var service = new StaticDataService();
            service.Load();
            _services.RegisterSingle<IStaticDataService>(service);
        }
    }
}