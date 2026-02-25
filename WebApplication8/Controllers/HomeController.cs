using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers;

public class HomeController : Controller
{
    private ITaskRepository _repo;
    
    public HomeController(ITaskRepository temp)
    {
        _repo = temp;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(new Task());
    }

    [HttpPost]
    public IActionResult Index(Task t)
    {
        if (ModelState.IsValid)
        {
            _repo.AddTask(t);
        }

        return View(new Task());
    }
    
    

   
}