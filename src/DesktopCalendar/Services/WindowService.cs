using System.Windows;
using DesktopCalendar.Interfaces;

namespace DesktopCalendar.Services
{
    internal class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowWindow(object viewModel)
        {
            if (viewModel == null) return;
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == viewModel);
            window?.Show();
        }
        public void HideWindow(object viewModel)
        {
            if (viewModel == null) return;
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == viewModel);
            window?.Hide();
        }
        public void CloseWindow(object viewModel)
        {
            if (viewModel == null) return;
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == viewModel);
            window?.Close();
        }
        public void CloseAllAndExit()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }
            Application.Current.Shutdown();
        }
    }
}
