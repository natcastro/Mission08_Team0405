using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication8.Models
{
    public class Task
    {
        [Key]
        public int TaskItemId { get; set; }

        [Required]
        public string TaskName { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }

        [Required]
        [Range(1, 4)]
        public int Quadrant { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public bool Completed { get; set; } = false;
    }

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}