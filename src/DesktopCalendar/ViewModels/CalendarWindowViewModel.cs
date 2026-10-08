using CalendarDayCell.Models;
using CalendarDayCell.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using DesktopCalendar.Views;

namespace DesktopCalendar.ViewModels
{
    internal partial class CalendarWindowViewModel : ObservableObject
    {
        public AppSettingsViewModel AppSettings { get; }
        public ObservableCollection<CalendarDayCellViewModel> DaysList { get; set; } = new ObservableCollection<CalendarDayCellViewModel>();
        public CalendarWindowViewModel(AppSettingsViewModel appSettings)
        {
            AppSettings = appSettings;
            LoadMonthDays();
        }

        [ObservableProperty]
        private bool _isMousePenetrate;

        [ObservableProperty]
        private bool _isPinToDesktop;

        [ObservableProperty]
        private string _currentMonthText = string.Empty;

        private DateOnly _displayedMonth = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, 1);

        [RelayCommand]
        private void TogglePenetrate() => IsMousePenetrate = !IsMousePenetrate;

        [RelayCommand]
        private void TogglePinToDesktop() => IsPinToDesktop = !IsPinToDesktop;

        [RelayCommand]
        private void PreviousMonth()
        {
            _displayedMonth = _displayedMonth.AddMonths(-1);
            LoadMonthDays();
        }

        [RelayCommand]
        private void NextMonth()
        {
            _displayedMonth = _displayedMonth.AddMonths(1);
            LoadMonthDays();
        }

        private void LoadMonthDays()
        {
            // TODO 联网获取数据，并从中获取当前月份的日期信息(含调休版本)
            CurrentMonthText = $"{_displayedMonth.Year}年{_displayedMonth.Month}月";

            var firstDay = new DateOnly(_displayedMonth.Year, _displayedMonth.Month, 1);
            var mondayBasedOffset = ((int)firstDay.DayOfWeek + 6) % 7;
            var firstDisplayedDay = firstDay.AddDays(-mondayBasedOffset);

            // 固定 42 个格子，切月只更新数据，避免重建视觉树。
            const int cellCount = 42;
            if (DaysList.Count == 0)
            {
                for (var i = 0; i < cellCount; i++)
                {
                    DaysList.Add(new CalendarDayCellViewModel(
                        new CalendarDayInput(firstDisplayedDay.AddDays(i), null)));
                }
                return;
            }
            for (var i = 0; i < cellCount; i++)
            {
                DaysList[i].SyncValues(new CalendarDayInput(firstDisplayedDay.AddDays(i), null));
            }
        }
        [RelayCommand]
        private void AddTodo(CalendarDayCellViewModel cellVm)
        {
            // TODO: 打开创建待办事项的对话框
            var dialog = App.Services.GetRequiredService<TodoEditWindow>();
            dialog.ShowDialog();
            //MessageBox.Show($"添加待办事项: {cellVm.DayNumber}");
        }
    }
}
