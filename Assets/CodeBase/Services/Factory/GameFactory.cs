using CodeBase.BehaviourTree.Brains;
using CodeBase.Camera;
using CodeBase.Data.Items;
using CodeBase.Enemy;
using CodeBase.Items;
using CodeBase.Items.Weapon.Firearms;
using CodeBase.Logic.ApplyDamage;
using CodeBase.Player;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.Observer;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.AttackFX;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Items.Weapon.Firearms;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInputService _inputService;
        private readonly IStaticDataService _dataService;
        private readonly ILogicFactoryService _logicFactory;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IGameObserverService _gameObserver;
        private readonly ISaveLoadProgressService _saveLoadProgress;
        private UnityEngine.Camera _mainCamera;

        public GameFactory(IInputService inputService,
            IStaticDataService dataService,
            ILogicFactoryService logicFactory,
            IPersistentProgressService persistentProgress,
            IGameObserverService gameObserver,
            ISaveLoadProgressService saveLoadProgress)
        {
            _inputService = inputService;
            _dataService = dataService;
            _logicFactory = logicFactory;
            _persistentProgress = persistentProgress;
            _gameObserver = gameObserver;
            _saveLoadProgress = saveLoadProgress;
        }

        public void SetCamera(UnityEngine.Camera mainCamera) =>
            _mainCamera = mainCamera;

        public GameObject CreatePlayer(Vector3 at, EnemyHealth[] enemyHealths)
        {
            GameObject playerInstance = Object.Instantiate(_dataService.PlayerStaticData.PlayerPrefab, at, Quaternion.identity);
            GameObject cameraBrainInstance = Object.Instantiate(_dataService.CameraStaticData.CameraBrainPrefab);

            playerInstance.GetComponent<PlayerEnableItemInHand>().Construct(_persistentProgress, this, _mainCamera);
            playerInstance.GetComponent<PlayerChangeSelectSlot>().Construct(_logicFactory, _inputService, _gameObserver);
            playerInstance.GetComponent<PlayerMove>().Construct(_inputService, _dataService, _gameObserver);
            playerInstance.GetComponent<PlayerHealth>().Construct(_gameObserver, _persistentProgress, _saveLoadProgress, _dataService);
            playerInstance.GetComponent<PlayerKillCounter>().Construct(_gameObserver, _persistentProgress, _saveLoadProgress, enemyHealths);
            cameraBrainInstance.GetComponent<CameraLookAndFollow>().Construct(_dataService, _inputService, _gameObserver, playerInstance.transform, _mainCamera);
            return playerInstance;
        }

        public GameObject CreateEnemy(EnemyID id, Vector3 at)
        {
            EnemyConfig config = _dataService.ForEnemy(id);
            GameObject instance = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instance.GetComponent<EnemyBrain>().Construct(config, _gameObserver);
            instance.GetComponent<EnemyAnimation>().Construct(_gameObserver);
            instance.GetComponentInChildren<EnemyFindTarget>().Construct(_gameObserver);
            instance.GetComponentInChildren<EnemyHealth>().Construct(config);
            return instance;
        }

        public ICloseItemInHand CreateItemInHand(BaseItemData item, Transform root)
        {
            switch (item)
            {
                case FirearmItemData firearm:
                    return CreateFirearm(firearm, root);

                default:
                    Debug.LogError($"I don't know how to create a {item} item");
                    return null;
            }
        }

        public void CreateFirearmFXAttack(SurfaceType surfaceType, Vector3 at, Vector3 normal)
        {
            AttackFXConfig config = _dataService.ForFirearmFX(surfaceType);
            Object.Instantiate(config.Prefab, at, Quaternion.FromToRotation(Vector3.forward, normal));
        }

        private FirearmsAttackHandler CreateFirearm(FirearmItemData data, Transform root)
        {
            FirearmItemConfig config = _dataService.ForFirearmsItems(data.ID);
            FirearmsAttackHandler instantiate = Object.Instantiate(config.PrefabInHand, root);
            instantiate.Construct(_inputService, _gameObserver, this, _mainCamera, data, config, _dataService.FirearmsStaticData);
            instantiate.GetComponent<FirearmsAnimator>().Construct(_inputService);
            return instantiate;
        }
    }
}