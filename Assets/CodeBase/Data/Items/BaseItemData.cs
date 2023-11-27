using CodeBase.StaticData.Items;
using System;
using UnityEngine;

namespace CodeBase.Data.Items
{
    public abstract class BaseItemData
    {
        [field: SerializeField] public ItemID ID { get; private set; }
        public Action OnValueChange;

        protected BaseItemData(ItemID id)
        {
            ID = id;
        }
    }
}