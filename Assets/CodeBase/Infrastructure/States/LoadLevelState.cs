using CodeBase.Enemy;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ILogicFactoryService _logicFactory;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _dataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadProgressService _saveLoad;
        private readonly IWindowService _windowService;
        private readonly IGameObserverService _gameObserver;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ILogicFactoryService logicFactory,
            IInputService inputService,
            IStaticDataService dataService,
            IPersistentProgressService progressService,
            ISaveLoadProgressService saveLoad,
            IWindowService windowService,
            IGameObserverService gameObserver)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _logicFactory = logicFactory;
            _inputService = inputService;
            _dataService = dataService;
            _progressService = progressService;
            _saveLoad = saveLoad;
            _windowService = windowService;
            _gameObserver = gameObserver;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            Clean();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;

            _gameFactory.SetCamera(mainCamera);
            _saveLoad.LoadProgress();
            _logicFactory.InitInventorySlotsHandler();

            HideCursor();
            InitUI();
            InitWorld();

            _stateMachine.Enter<LoopState>();
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
            _windowService.Open(WindowId.HUD);
            _windowService.Open(WindowId.Inventory);
        }

        private void InitWorld()
        {
            EnemyHealth[] enemyHealth = InitializeEnemy();
            _gameFactory.CreatePlayer(_dataService.LevelStaticData.PlayerInitialPosition, enemyHealth);
        }

        private EnemyHealth[] InitializeEnemy()
        {
            EnemyHealth[] healths = new EnemyHealth[_dataService.LevelStaticData.EnemyLevelSpawnData.Capacity];

            for (var i = 0; i < _dataService.LevelStaticData.EnemyLevelSpawnData.Count; i++)
            {
                EnemyLevelSpawnStaticData data = _dataService.LevelStaticData.EnemyLevelSpawnData[i];
                GameObject enemyInstance = _gameFactory.CreateEnemy(data.ID, data.Position);
                healths[i] = enemyInstance.GetComponentInChildren<EnemyHealth>();
            }
            return healths;
        }

        private void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Clean()
        {
            _uiFactory.Clean();
            _logicFactory.Cleanup();
            _inputService.Cleanup();
            _gameObserver.Cleanup();

            if (_progressService.PlayerProgress != null)
            {
                _progressService.PlayerProgress.WinLoseData?.Cleanup();
                _progressService.PlayerProgress.InventorySlots?.Cleanup();
            }
        }
    }
}