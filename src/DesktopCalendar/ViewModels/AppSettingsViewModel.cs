using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media.Imaging;
using System.IO;
using DesktopCalendar.Interfaces;

namespace DesktopCalendar.ViewModels
{
    internal partial class AppSettingsViewModel : ObservableObject
    {
        IWindowService _windowService;
        public AppSettingsViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            // 设置托盘图标加载
            string iconPath = Path.Combine(AppContext.BaseDirectory, @"Resources\Icons\logo.ico");
            if (File.Exists(iconPath))
            {
                TrayIconSource = BitmapFrame.Create(new Uri(iconPath, UriKind.Absolute));
            }
        }

        // 程序图标
        [ObservableProperty]
        private BitmapFrame? _trayIconSource;
        // 透明度设置
        [ObservableProperty]
        private double _opacity = 0.5;
        // 重置透明度
        [RelayCommand]
        public void ResetOpacity()
        {
            Opacity = 0.5;
        }
        // 退出
        [RelayCommand]
        private void CloseAll()
        {
            _windowService.CloseAllAndExit();
        }
    }
}
