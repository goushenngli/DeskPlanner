namespace TodoCalendar.Models
{
    public class TodoItemModel
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public TodoItemModel(string title, bool isCompleted, DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            Title = title;
            IsCompleted = isCompleted;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
