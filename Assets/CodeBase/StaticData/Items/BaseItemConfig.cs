using UnityEngine;

namespace CodeBase.StaticData.Items
{
    public class BaseItemConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public ItemID ID { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public void OnValidate()
        {
            _inspectorName = ID.ToString();
        }
    }
}