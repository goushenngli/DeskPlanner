using System.Collections.ObjectModel;
using TodoCalendar.Models;
using TodoCalendar.ViewModels;

namespace TodoCalendar.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TodoItemViewModel> Items { get; } = new ObservableCollection<TodoItemViewModel>();

        public MainViewModel()
        {
            // 添加一些示例数据
            Items.Add(new TodoItemViewModel("示例任务 1", false));
            Items.Add(new TodoItemViewModel("示例任务 2", true));
        }
    }
}
