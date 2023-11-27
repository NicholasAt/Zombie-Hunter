using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.HUD
{
    public class WinLoseCounterWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _loseText;
        private WinLoseData _winLoseData;

        public void Construct(IPersistentProgressService persistentProgress)
        {
            _winLoseData = persistentProgress.PlayerProgress.WinLoseData;

            _winLoseData.OnWinChange += Refresh;
            _winLoseData.OnLoseChange += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            _winLoseData.OnWinChange -= Refresh;
            _winLoseData.OnLoseChange -= Refresh;
        }

        private void Refresh()
        {
            _winText.text = $"Win: {_winLoseData.WinCount}";
            _loseText.text = $"Lose: {_winLoseData.LoseCount}";
        }
    }
}