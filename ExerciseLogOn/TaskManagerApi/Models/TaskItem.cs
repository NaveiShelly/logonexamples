using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title length can't be more than 100 characters")]
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "Description can't be more than 300 characters")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
    }
}
