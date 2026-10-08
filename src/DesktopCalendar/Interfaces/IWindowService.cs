namespace DesktopCalendar.Interfaces
{
    public interface IWindowService
    {
        void ShowWindow(object viewModel);
        void HideWindow(object viewModel);
        void CloseWindow(object viewModel);
        void CloseAllAndExit();
    }
}
