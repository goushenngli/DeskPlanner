using CommunityToolkit.Mvvm.ComponentModel;

namespace TodoCalendar.ViewModels
{
    public partial class TodoItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isCompleted;
        [ObservableProperty]
        private string _title;
        public TodoItemViewModel(string title, bool isCompleted = false)
        {
            _title = title;
            _isCompleted = isCompleted;
        }
    }
}
