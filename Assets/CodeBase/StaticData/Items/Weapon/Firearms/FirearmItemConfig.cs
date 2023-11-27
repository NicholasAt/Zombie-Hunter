using CodeBase.Items.Weapon.Firearms;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Items.Weapon.Firearms
{
    [Serializable]
    public class FirearmItemConfig : BaseItemConfig
    {
        [field: SerializeField] public FirearmsAttackHandler PrefabInHand { get; private set; }
        [field: SerializeField] public FirearmsShootID ShootID { get; private set; }
        [field: SerializeField] public float ShootDelay { get; private set; } = 1f;
        [field: SerializeField] public float Damage { get; private set; } = 100f;
    }
}