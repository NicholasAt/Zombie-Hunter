using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.Data.Items
{
    [Serializable]
    public class FirearmItemData : BaseItemData
    {
        [field: SerializeField] public int CurrentAmmo { get; private set; }

        public FirearmItemData(int currentAmmo, ItemID id) : base(id)
        {
            CurrentAmmo = currentAmmo;
        }

        public void IncrementAmmo()
        {
            CurrentAmmo--;
            OnValueChange?.Invoke();
        }
    }
}