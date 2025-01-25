using Microsoft.AspNetCore.Mvc;

namespace TaskmanagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddTask()
        {
            return View();
        }
    }
}
