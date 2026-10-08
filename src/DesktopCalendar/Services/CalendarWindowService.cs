using DesktopCalendar.Interfaces;
using DesktopCalendar.ViewModels;
using DesktopCalendar.Views;

namespace DesktopCalendar.Services
{
    internal class CalendarWindowService : ICalendarWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        public CalendarWindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowCalendarWindow()
        {
            var window = _serviceProvider.GetOrCreateWindow<CalendarWindow>();
            window?.Show();
        }
        public void HideCalendarWindow()
        {
            var window = _serviceProvider.GetOrCreateWindow<CalendarWindow>();
            window?.Hide();
        }

        public void TogglePenetrate()
        {
            var viewModel = GetCalendarWindowViewModel();
            viewModel?.TogglePenetrateCommand.Execute(null);
        }

        public void TogglePinToDesktop()
        {
            var viewModel = GetCalendarWindowViewModel();
            viewModel?.TogglePinToDesktopCommand.Execute(null);
        }

        private CalendarWindowViewModel? GetCalendarWindowViewModel()
        {
            var window = _serviceProvider.GetOrCreateWindow<CalendarWindow>();
            return window?.DataContext as CalendarWindowViewModel;
        }
    }
}
