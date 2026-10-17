using DesktopCalendar.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopCalendar.Views
{
    /// <summary>
    /// TodoEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TodoEditWindow : Window
    {
        public TodoEditWindow()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<TodoEditWindowViewModel>();
        }
        public TodoEditWindow(Window owner) : this()
        {
            Owner = owner;
        }
    }
}
