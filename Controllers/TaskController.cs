using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using task_tracker.Models;
using task_tracker.Data;

namespace task_tracker.Controllers;

public class TaskController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly TaskDbContext _context;

  public TaskController(ILogger<HomeController> logger, TaskDbContext context)

  {
    _logger = logger;
    _context = context;
  }

  public IActionResult Index()
  {
    var tasks = _context.Tasks.ToList();
    return View(tasks);
  }

  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Create(TaskTodo task)
  {
    if (ModelState.IsValid)
    {
      _context.Tasks.Add(task);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    return View(task);

  }
  public IActionResult Delete(int id)
  {
    var task = _context.Tasks.Find(id);
    if (task == null)
      return NotFound();

    return View(task);
  }

  [HttpPost, ActionName("Delete")]
  public IActionResult DeleteConfirmed(int id)
  {
    var task = _context.Tasks.Find(id);
    if (task == null) return NotFound();
    _context.Tasks.Remove(task);
    _context.SaveChanges();
    return RedirectToAction(nameof(Index));
  }

  public IActionResult Details(int id)
  {
    var task = _context.Tasks.Find(id);
    if (task == null)
      return NotFound();
    return View(task);
  }

  public IActionResult Edit(int id)
  {
    var task = _context.Tasks.Find(id);
    if (task == null) return NotFound();
    return View(task);
  }

  [HttpPost]
  public IActionResult Edit(TaskTodo task)
  {
    if (ModelState.IsValid)
    {
      _context.Tasks.Update(task);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
    }
    return View(task);
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
