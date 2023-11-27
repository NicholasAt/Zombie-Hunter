using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window static data")]
    public class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UIRoot { get; private set; }

        public List<WindowConfig> Configs;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}