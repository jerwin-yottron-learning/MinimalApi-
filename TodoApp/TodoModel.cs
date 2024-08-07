using System.ComponentModel.DataAnnotations;

namespace TodoApp
{
    public class TodoModel
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool IsTaskCompleted { get; set; }
    }
}
