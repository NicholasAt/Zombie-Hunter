using CodeBase.StaticData.AttackFX;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Items.Weapon.Firearms
{
    [CreateAssetMenu(menuName = "Static Data/Items/Firearms Static Data", order = 0)]
    public class FirearmsStaticData : ScriptableObject
    {
        [field: SerializeField] public AttackFXStaticData FXStaticData { get; private set; }
        [field: SerializeField] public LayerMask AttackLayers { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; } = 100;

        public List<FirearmItemConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}