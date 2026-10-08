using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopCalendar.Interfaces;

namespace DesktopCalendar.ViewModels
{
    internal partial class SettingsWindowViewModel : ObservableObject
    {
        private readonly IWindowService _windowService;
        public AppSettingsViewModel AppSettings { get; }
        public SettingsWindowViewModel(IWindowService windowService, AppSettingsViewModel appSettings)
        {
            _windowService = windowService;
            AppSettings = appSettings;
        }
        // 退出
        [RelayCommand]
        private void Exit()
        {
            _windowService.CloseWindow(this);
        }
    }
}
