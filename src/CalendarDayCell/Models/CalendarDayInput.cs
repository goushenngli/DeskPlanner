using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarDayCell.Models
{
    public readonly record struct CalendarDayInput(
        DateOnly Date,
        IReadOnlyList<string>? CustomTodoTitles = null
    );
}
