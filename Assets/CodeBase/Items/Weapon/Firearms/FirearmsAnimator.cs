using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Items.Weapon.Firearms
{
    public class FirearmsAnimator : MonoBehaviour
    {
        private readonly int MoveHash = Animator.StringToHash("SpeedMove");
        private readonly int AttackHash = Animator.StringToHash("Attack");

        [SerializeField] private Animator _animator;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            UpdateMove();
        }

        private void UpdateMove()
        {
            float runningSpeed = (_inputService.MoveAxis != Vector2.zero && _inputService.IsSprint) ? 1f : 0f;
            _animator.SetFloat(MoveHash, runningSpeed, 0.4f, Time.deltaTime); //damp time
        }

        public void PlayAttack()
        {
            _animator.CrossFade(AttackHash, 0.1f);//normalize duration
        }
    }
}