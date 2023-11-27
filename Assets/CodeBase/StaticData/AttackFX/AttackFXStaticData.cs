using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.AttackFX
{
    [CreateAssetMenu(menuName = "Static Data/Attack Fx Static Data", order = 0)]
    public class AttackFXStaticData : ScriptableObject
    {
        public List<AttackFXConfig> FXConfigs;

        private void OnValidate()
        {
            FXConfigs.ForEach(x => x.OnValidate());
        }
    }
}