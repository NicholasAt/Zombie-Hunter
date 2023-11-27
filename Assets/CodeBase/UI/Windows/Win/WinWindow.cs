namespace CodeBase.UI.Windows.Win
{
    public class WinWindow : BaseWindow
    {
        private void Start()
        {
            Invoke(nameof(CloseThis), 2f);//delay to close
        }

        private void CloseThis() =>
            Close();
    }
}