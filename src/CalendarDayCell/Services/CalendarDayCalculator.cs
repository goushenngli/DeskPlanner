using CalendarDayCell.Models;
using TodoCalendar.Models;

namespace CalendarDayCell.Services
{
    public static class CalendarDayCalculator
    {
        public static CalendarDayValue Calculate(CalendarDayInput input)
        {
            var date = input.Date;

            // 1. 日期计算
            string dayNumber = date.Day.ToString();

            // 2. 节假日匹配算法
            string festivalName = (date.Month, date.Day) switch
            {
                (10, 1) => "国庆节",
                (1, 1) => "元旦",
                _ => string.Empty
            };

            // 3. 班休匹配算法
            string workStatusText = date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ? "休" : "班";

            // 4. 待办转化
            var todos = input.CustomTodoTitles?.Select(t => new TodoItemModel(t, false, null, null)).ToList()
                        ?? new List<TodoItemModel>();

            // TODO: 测试用，待删除
            todos.Add(new TodoItemModel("111默认待办事项1", false, null, null));
            todos.Add(new TodoItemModel("222默认待办事项2", false, null, null));
            todos.Add(new TodoItemModel("333默认待办事项3", false, null, null));
            todos.Add(new TodoItemModel("444默认待办事项4", false, null, null));
            todos.Add(new TodoItemModel("555默认待办事项5", false, null, null));
            todos.Add(new TodoItemModel("666默认待办事项6", false, null, null));
            todos.Add(new TodoItemModel("777默认待办事项7", false, null, null));

            return new CalendarDayValue(dayNumber, festivalName, workStatusText, todos);
        }
    }
}
