using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopCalendar.Interfaces;

namespace DesktopCalendar.ViewModels
{
    internal partial class TrayViewModel : ObservableObject
    {
        private readonly ICalendarWindowService _calendarWindowService;
        private readonly ISettingsService _settingsService;
        private readonly IWindowService _windowService;

        public AppSettingsViewModel AppSettings { get; }

        public TrayViewModel(
            ICalendarWindowService calendarWindowService,
            ISettingsService settingsService,
            IWindowService windowService,
            AppSettingsViewModel appSettings)
        {
            _calendarWindowService = calendarWindowService;
            _settingsService = settingsService;
            _windowService = windowService;
            AppSettings = appSettings;
        }

        [RelayCommand]
        private void ShowCalendarWindow() => _calendarWindowService.ShowCalendarWindow();

        [RelayCommand]
        private void HideCalendarWindow() => _calendarWindowService.HideCalendarWindow();

        [RelayCommand]
        private void TogglePenetrate() => _calendarWindowService.TogglePenetrate();

        [RelayCommand]
        private void TogglePinToDesktop() => _calendarWindowService.TogglePinToDesktop();

        [RelayCommand]
        private void OpenSettings() => _settingsService.ShowSettings();

        [RelayCommand]
        private void ExitAll() => _windowService.CloseAllAndExit();
    }
}
