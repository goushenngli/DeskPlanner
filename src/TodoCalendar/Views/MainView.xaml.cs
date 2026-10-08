using System.Windows.Controls;
using TodoCalendar.Helpers;

namespace TodoCalendar.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Main;
        }
    }
}
