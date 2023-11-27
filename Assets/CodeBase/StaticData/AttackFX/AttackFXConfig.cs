using CodeBase.Logic.ApplyDamage;
using System;
using UnityEngine;

namespace CodeBase.StaticData.AttackFX
{
    [Serializable]
    public class AttackFXConfig
    {
        [SerializeField] private string _incpectorName;
        [field: SerializeField] public SurfaceType SurfaceType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate() =>
            _incpectorName = SurfaceType.ToString();
    }
}