using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using DesktopCalendar.ViewModels;

namespace DesktopCalendar.Views
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<SettingsWindowViewModel>();
        }
        // 通过构造器委托调用无参构造器,所以不需要再写InitializeComponent();
        public SettingsWindow(Window owner) : this()
        {
            this.Owner = owner;
        }
    }
}
