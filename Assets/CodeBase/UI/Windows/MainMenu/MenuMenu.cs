using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using static CodeBase.Data.GameConstants;

namespace CodeBase.UI.Windows.MainMenu
{
    public class MenuMenu : BaseWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _playButton.onClick.AddListener(() => gameStateMachine.Enter<LoadLevelState, string>(GameSceneKey));
            _exitButton.onClick.AddListener(Application.Quit);
        }
    }
}