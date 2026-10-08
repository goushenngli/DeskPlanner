using TodoCalendar.ViewModels;

namespace TodoCalendar.Helpers
{
    // 简单的 ViewModelLocator，便于 XAML 绑定设计时和运行时视图模型
    public static class ViewModelLocator
    {
        private static MainViewModel? _main;
        public static MainViewModel Main => _main ??= new MainViewModel();
    }
}
