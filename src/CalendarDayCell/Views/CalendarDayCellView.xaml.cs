using System.Windows;
using System.Windows.Controls;
// TODO: 双击编辑页面
// TODO: 鼠标划过显示添加按钮
// TODO: 今天的日期显示为红色(特殊显示)
// TODO: 被选中效果

namespace CalendarDayCell.Views
{
    /// <summary>
    /// Interaction logic for CalendarDayCellView.xaml
    /// </summary>
    public partial class CalendarDayCellView : UserControl
    {
        public CalendarDayCellView()
        {
            InitializeComponent();
        }
        // 对外发布添加待办事项请求
        public event EventHandler? AddRequested;
        private void RequestAddTodo(object sender, RoutedEventArgs e)
        {
            AddRequested?.Invoke(this, EventArgs.Empty);
        }
    }

}
