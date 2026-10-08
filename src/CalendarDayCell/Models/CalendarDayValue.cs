using TodoCalendar.Models;

namespace CalendarDayCell.Models
{
    public class CalendarDayValue
    {
        public string DayNumber;
        public string FestivalName;
        public string WorkStatusText;
        public IReadOnlyList<TodoItemModel> TodoList;
        public CalendarDayValue(string dayNumber, string festivalName, string workStatusText, IReadOnlyList<TodoItemModel> todoList)
        {
            DayNumber = dayNumber;
            FestivalName = festivalName;
            WorkStatusText = workStatusText;
            TodoList = todoList;
        }
    }
}
