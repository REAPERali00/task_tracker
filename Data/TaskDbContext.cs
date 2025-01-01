using task_tracker.Models;
using Microsoft.EntityFrameworkCore;
namespace task_tracker.Data;

public class TaskDbContext : DbContext
{

  public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
  {
  }
  public DbSet<TaskTodo> Tasks { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TaskTodo>().ToTable("Tasks");
  }

}
