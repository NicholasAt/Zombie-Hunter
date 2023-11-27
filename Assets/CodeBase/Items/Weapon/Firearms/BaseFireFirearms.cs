using System;
using UnityEngine;

namespace CodeBase.Items.Weapon.Firearms
{
    public abstract class BaseFireFirearms
    {
        protected readonly Action OnFire;
        protected readonly float ShootDelay;

        protected BaseFireFirearms(Action onFire, float shootDelay)
        {
            ShootDelay = shootDelay;
            OnFire = onFire;
        }

        public abstract void UpdateAttack(bool isFirePress);
    }

    public class AutomaticShootFirearms : BaseFireFirearms
    {
        private float _currentDelay;

        public AutomaticShootFirearms(Action onFire, float shootDelay) : base(onFire, shootDelay)
        { }

        public override void UpdateAttack(bool isFirePress)
        {
            _currentDelay -= Time.deltaTime;

            if (isFirePress && _currentDelay <= 0)
            {
                _currentDelay = ShootDelay;
                OnFire.Invoke();
            }
        }
    }

    public class SingleShootFirearms : BaseFireFirearms
    {
        private bool _singleShoot;
        private float _currentDelay;

        public SingleShootFirearms(Action onFire, float shootDelay) : base(onFire, shootDelay)
        { }

        public override void UpdateAttack(bool isFirePress)
        {
            _currentDelay -= Time.deltaTime;

            if (isFirePress && _singleShoot == false)
            {
                _singleShoot = true;
                _currentDelay = ShootDelay;
                OnFire.Invoke();
            }
            else if (isFirePress == false && _singleShoot && _currentDelay <= 0)
            {
                _singleShoot = false;
            }
        }
    }
}