# Task Tracker

Here’s a simple practice project for **ASP.NET MVC** that you can complete within a day, including database integration.

---

### **Project Title: Task Tracker**

**Objective**: Create a basic Task Tracker app where users can create, view, edit, and delete tasks.

---

### **Features**

1. **CRUD Operations**:

   - Create a new task.
   - View all tasks.
   - Update an existing task.
   - Delete a task.

2. **Database**:

   - Use Entity Framework Core with a SQLite or SQL Server database.
   - Store tasks with the following properties:
     - `Id` (int, Primary Key)
     - `Title` (string, max 100 characters)
     - `Description` (string, max 500 characters)
     - `DueDate` (DateTime)
     - `IsCompleted` (bool)

3. **Pages**:
   - **Home Page**: List all tasks with options to edit or delete.
   - **Add/Edit Task Page**: Form to add or edit a task.
   - **Details Page**: View task details.

---

### **Step-by-Step Guide**

#### **1. Set Up the Project**

- Create a new ASP.NET MVC project:

  ```bash
  dotnet new mvc -n TaskTracker
  cd TaskTracker
  ```

- Add Entity Framework Core packages:

  ```bash
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  ```

---

#### **2. Create the Models**

Create a `Task` model in `Models/Task.cs`:

```csharp
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
```

---

#### **3. Set Up the Database Context**

Create a `TaskContext` in `Data/TaskContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
    }
}
```

---

#### **4. Configure the Database**

In `Program.cs`, add the database context:

```csharp
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

var app = builder.Build();
```

Run migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

#### **5. Create the Controller**

Create a `TaskController` in `Controllers/TaskController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        public IActionResult Details(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
```

---

#### **6. Create the Views**

Generate views for the actions using the **Razor Pages** engine.

- **Index.cshtml**: List all tasks with "Create", "Edit", "Details", and "Delete" buttons.
- **Create.cshtml**: Form to add a new task.
- **Edit.cshtml**: Form to update an existing task.
- **Details.cshtml**: Display the details of a specific task.
- **Delete.cshtml**: Confirmation page for deleting a task.

Run scaffolding to auto-generate views:

```bash
dotnet aspnet-codegenerator controller -name TaskController -m TaskTracker.Models.Task -dc TaskTracker.Data.TaskContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name TaskController -m task_tracker.Models.TaskTodo -dc task_tracker.Data.TaskDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```

---

#### **7. Test the App**

Run the app:

```bash
dotnet run
```

Access it at `http://localhost:5000/Task`.

---

### **Bonus Challenges**

1. Add a filter to show only completed or pending tasks.
2. Allow sorting tasks by `DueDate` or `Title`.
3. Add Bootstrap for better styling.

This project provides a full CRUD experience with database integration, and you can complete it within a day!
