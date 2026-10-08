using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using DesktopCalendar.ViewModels;

namespace DesktopCalendar.Views
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        private AppSettingsViewModel AppSettings { get; }
        public CalendarWindow()
        {
            InitializeComponent();
            // 鼠标拖动效果
            this.MouseDown += Grid_WindowsMove;
            DataContext = App.Services.GetRequiredService<CalendarWindowViewModel>();
            AppSettings = App.Services.GetRequiredService<AppSettingsViewModel>();
        }
        private void Grid_WindowsMove(object sender, MouseButtonEventArgs e)
        {
            // TODO 通过右键锁定按钮/页面锁定按钮 确认是否启用拖动窗口功能
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO 测试用,待删除
            this.Close();
        }

        private void set_Click(object sender, RoutedEventArgs e)
        {
            // TODO 测试用,待删除
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}