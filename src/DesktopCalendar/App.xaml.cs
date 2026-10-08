using H.NotifyIcon;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using DesktopCalendar.Interfaces;
using DesktopCalendar.Services;
using DesktopCalendar.ViewModels;
using DesktopCalendar.Views;

namespace DesktopCalendar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;
        public static object? TrayViewModel { get; private set; }
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // TODO 设置为可选项
            // 强制使用软件渲染,可降低内存占用,但可能会降低性能
            //RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;

            var trayViewModel = Services.GetRequiredService<TrayViewModel>();
            TrayViewModel = trayViewModel;

            var calendarWindow = Services.GetRequiredService<CalendarWindow>();
            var trayIcon = (TaskbarIcon)Resources["TrayIcon"];

            trayIcon.DataContext = trayViewModel;
            trayIcon.IconSource = trayViewModel.AppSettings.TrayIconSource;
            trayIcon.ForceCreate();

            MainWindow = calendarWindow;
            calendarWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            // 销毁托盘
            var trayIcon = (TaskbarIcon)Resources["TrayIcon"];
            trayIcon?.Dispose();

            base.OnExit(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 注册服务接口和实现
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<ICalendarWindowService, CalendarWindowService>();
            // 注册公共 ViewModel 为单例 (Singleton)
            services.AddSingleton<AppSettingsViewModel>();
            services.AddSingleton<TrayViewModel>();
            // 注册各页面的 View 为 transient
            services.AddTransient<CalendarWindow>();
            services.AddTransient<SettingsWindow>();
            services.AddTransient<TodoEditWindow>();
            // 注册各页面的私有 ViewModel 为 transient
            services.AddTransient<CalendarWindowViewModel>();
            services.AddTransient<SettingsWindowViewModel>();
        }
    }

}
