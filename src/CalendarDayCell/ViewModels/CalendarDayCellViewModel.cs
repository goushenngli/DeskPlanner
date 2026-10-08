using CalendarDayCell.Models;
using CalendarDayCell.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TodoCalendar.Models;
using TodoCalendar.ViewModels;

namespace CalendarDayCell.ViewModels;

/// <summary>
/// View model for one calendar day cell.
/// The parameterless constructor provides sample data for the XAML designer.
/// </summary>
public partial class CalendarDayCellViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isMouseEnter;
    [ObservableProperty]
    private CalendarDayInput _input;
    [ObservableProperty]
    private string _dayNumber = string.Empty;
    [ObservableProperty]
    private string _festivalName = string.Empty;
    [ObservableProperty]
    private string _workStatusText = string.Empty;
    [ObservableProperty]
    private ObservableCollection<TodoItemViewModel> _todoItems = new ObservableCollection<TodoItemViewModel>();
    public CalendarDayCellViewModel() : this(new CalendarDayInput(DateOnly.FromDateTime(DateTime.Today))) 
    { 
    }
    public CalendarDayCellViewModel(CalendarDayInput input)
    {
        SyncValues(input);
    }
    public void SyncValues(CalendarDayInput input)
    {
        Input = input;
        var value = CalendarDayCalculator.Calculate(input);
        DayNumber = value.DayNumber;
        FestivalName = value.FestivalName;
        WorkStatusText = value.WorkStatusText;
        // TODO: 拿到日期后筛选在ui范围内的todo列表
        SyncTodos(value.TodoList);
    }

    private void SyncTodos(IReadOnlyList<TodoItemModel> todos)
    {
        // 尽可能复用现有的 TodoItemViewModel 实例，避免不必要的对象创建和销毁
        var sharedCount = Math.Min(TodoItems.Count, todos.Count);
        for (var i = 0; i < sharedCount; i++)
        {
            TodoItems[i].Title = todos[i].Title;
            TodoItems[i].IsCompleted = todos[i].IsCompleted;
        }

        while (TodoItems.Count < todos.Count)
        {
            var todo = todos[TodoItems.Count];
            TodoItems.Add(new TodoItemViewModel(todo.Title, todo.IsCompleted));
        }

        while (TodoItems.Count > todos.Count)
        {
            TodoItems.RemoveAt(TodoItems.Count - 1);
        }
    }
}

