using CodeBase.Services.Input;
using CodeBase.Services.Observer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Camera;
using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraLookAndFollow : MonoBehaviour
    {
        [SerializeField] private float _offsetY = 1.4f;

        private float _xRotation;
        private float _yRotation;
        private CameraStaticData _config;
        private IInputService _inputService;
        private Transform _target;
        private Transform _cameraTransform;
        private IGameObserverService _gameObserver;

        public void Construct(IStaticDataService dataService, IInputService inputService, IGameObserverService gameObserver, Transform target, UnityEngine.Camera mainCamera)
        {
            _config = dataService.CameraStaticData;
            _inputService = inputService;
            _gameObserver = gameObserver;

            _target = target;
            _cameraTransform = mainCamera.transform;
            _cameraTransform.position = new Vector3(target.position.x, _offsetY, target.position.z);
            _gameObserver.OnPlayerLose += Lose;
        }

        private void Update()
        {
            Look();
        }

        private void LateUpdate()
        {
            RotateTarget();
            FollowTarget();
        }

        private void OnDestroy()
        {
            _gameObserver.OnPlayerLose -= Lose;
        }

        private void RotateTarget()
        {
            _target.eulerAngles = new Vector3(0, _cameraTransform.eulerAngles.y, 0);
        }

        private void FollowTarget()
        {
            _cameraTransform.position = _target.position + new Vector3(0, _offsetY);
        }

        private void Look()
        {
            _xRotation -= _inputService.CameraAxis.y;
            _yRotation += _inputService.CameraAxis.x;

            _xRotation = Mathf.Clamp(_xRotation, _config.XClampDown, _config.XClampUp);
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }

        private void Lose()
        {
            enabled = false;
        }
    }
}