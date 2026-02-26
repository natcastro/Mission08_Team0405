using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;
using TaskModel = WebApplication8.Models.Task;

namespace WebApplication8.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repo;

    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return RedirectToAction(nameof(Quadrants));
    }

    [HttpGet]
    public IActionResult CreateTask()
    {
        LoadCategories();
        ViewBag.FormAction = nameof(CreateTask);
        ViewBag.PageTitle = "Add Task";
        return View("createTask", new TaskModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateTask(TaskModel task)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories();
            ViewBag.FormAction = nameof(CreateTask);
            ViewBag.PageTitle = "Add Task";
            return View("createTask", task);
        }

        _repo.AddTask(task);
        _repo.Save();
        return RedirectToAction(nameof(Quadrants));
    }

    [HttpGet]
    public IActionResult Quadrants()
    {
        var tasks = _repo.Tasks
            .Where(t => !t.Completed)
            .ToList();

        return View(tasks);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _repo.Tasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task == null)
        {
            return NotFound();
        }

        LoadCategories();
        ViewBag.FormAction = nameof(Edit);
        ViewBag.PageTitle = "Edit Task";
        return View("createTask", task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(TaskModel task)
    {
        if (!ModelState.IsValid)
        {
            LoadCategories();
            ViewBag.FormAction = nameof(Edit);
            ViewBag.PageTitle = "Edit Task";
            return View("createTask", task);
        }

        _repo.UpdateTask(task);
        _repo.Save();
        return RedirectToAction(nameof(Quadrants));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var task = _repo.Tasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task == null)
        {
            return NotFound();
        }

        _repo.DeleteTask(task);
        _repo.Save();
        return RedirectToAction(nameof(Quadrants));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete(int id)
    {
        var task = _repo.Tasks.FirstOrDefault(t => t.TaskItemId == id);
        if (task == null)
        {
            return NotFound();
        }

        task.Completed = true;
        _repo.UpdateTask(task);
        _repo.Save();
        return RedirectToAction(nameof(Quadrants));
    }

    private void LoadCategories()
    {
        ViewBag.Categories = _repo.Categories.OrderBy(c => c.CategoryName).ToList();
    }
}
