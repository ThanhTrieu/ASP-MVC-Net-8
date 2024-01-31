using Microsoft.AspNetCore.Mvc;

namespace TrainingFPTCo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
