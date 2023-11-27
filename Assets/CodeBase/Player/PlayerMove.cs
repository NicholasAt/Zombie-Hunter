using CodeBase.Services.Input;
using CodeBase.Services.Observer;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ConstantForce _constantForce;

        private IInputService _input;
        private PlayerStaticData _config;
        private IGameObserverService _gameObserver;

        public void Construct(IInputService inputService, IStaticDataService dataService, IGameObserverService gameObserver)
        {
            _input = inputService;
            _config = dataService.PlayerStaticData;
            _gameObserver = gameObserver;

            _constantForce.force = new Vector3(_constantForce.force.x, _config.ConstantForceY, _constantForce.force.z);

            _input.OnJump += Jump;
            _gameObserver.OnPlayerLose += Lose;
        }

        private void OnDestroy()
        {
            _input.OnJump -= Jump;
            _gameObserver.OnPlayerLose -= Lose;
        }

        private void FixedUpdate()
        {
            UpdateMove();
        }

        private void Lose()
        {
            enabled = false;
        }

        private void UpdateMove()
        {
            Vector3 direction = transform.forward * _input.MoveAxis.y + transform.right * _input.MoveAxis.x;
            direction *= (_input.IsSprint ? _config.SprintSpeed : _config.Speed) * 100 * Time.fixedDeltaTime;//extra speed
            _rigidbody.velocity = new Vector3(direction.x, _rigidbody.velocity.y, direction.z);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * _config.JumpForce, ForceMode.VelocityChange);
        }
    }
}