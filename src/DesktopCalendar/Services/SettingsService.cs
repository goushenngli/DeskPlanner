using DesktopCalendar.Views;
using DesktopCalendar.Interfaces;

namespace DesktopCalendar.Services
{
    internal class SettingsService : ISettingsService
    {
        private readonly IServiceProvider _serviceProvider;
        public SettingsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowSettings()
        {
            var settingsWindow = _serviceProvider.GetOrCreateWindow<SettingsWindow>();
            settingsWindow?.Show();
        }
    }
}
