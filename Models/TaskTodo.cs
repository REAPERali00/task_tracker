using System.ComponentModel.DataAnnotations;
namespace task_tracker.Models;

public class TaskTodo
{
  public int Id { get; set; }

  [Required]
  [StringLength(500, MinimumLength = 5)]
  public string Description { get; set; }

  public bool IsCompleted { get; set; } = false;

  [DataType(DataType.Date)]
  public DateTime DueDate { get; set; }

}
