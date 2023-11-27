using CodeBase.Data.Items;
using CodeBase.Logic.ApplyDamage;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.Observer;
using CodeBase.StaticData.Items.Weapon.Firearms;
using System;
using UnityEngine;

namespace CodeBase.Items.Weapon.Firearms
{
    public class FirearmsAttackHandler : MonoBehaviour, ICloseItemInHand
    {
        [SerializeField] private FirearmsAnimator _animator;

        private BaseFireFirearms _currentFireBuild;
        private IInputService _inputService;
        private FirearmItemData _weaponData;
        private UnityEngine.Camera _mainCamera;
        private FirearmItemConfig _config;
        private FirearmsStaticData _firearmsStaticData;
        private IGameObserverService _gameObserver;
        private IGameFactory _gameFactory;

        public void Construct(IInputService inputService, IGameObserverService gameObserver, IGameFactory gameFactory, UnityEngine.Camera mainCamera, FirearmItemData itemData, FirearmItemConfig config, FirearmsStaticData firearmsStaticData)
        {
            _inputService = inputService;
            _mainCamera = mainCamera;
            _weaponData = itemData;
            _config = config;
            _gameObserver = gameObserver;
            _firearmsStaticData = firearmsStaticData;
            _gameFactory = gameFactory;

            _gameObserver.OnPlayerLose += Lose;

            switch (_config.ShootID)
            {
                case FirearmsShootID.None:
                    break;

                case FirearmsShootID.Single:
                    _currentFireBuild = new SingleShootFirearms(FireProcess, config.ShootDelay);
                    break;

                case FirearmsShootID.Automatic:
                    _currentFireBuild = new AutomaticShootFirearms(FireProcess, config.ShootDelay);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(config.ShootID), config.ShootID, null);
            }
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= Lose;
        }

        public void Update()
        {
            _currentFireBuild.UpdateAttack(_inputService.UseItem1);
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        private void FireProcess()
        {
            _weaponData.IncrementAmmo();
            Transform cameraTransform = _mainCamera.transform;

            _animator.PlayAttack();
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _firearmsStaticData.AttackDistance, _firearmsStaticData.AttackLayers, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.TryGetComponent(out IApplyDamage applyDamage))
                {
                    applyDamage.ApplyDamage(_config.Damage);
                    _gameFactory.CreateFirearmFXAttack(applyDamage.SurfaceType, hit.point, hit.normal);
                }
            }
        }

        private void Lose()
        {
            enabled = false;
        }
    }
}