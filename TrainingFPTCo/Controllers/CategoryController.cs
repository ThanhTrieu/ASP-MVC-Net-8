using Microsoft.AspNetCore.Mvc;

namespace TrainingFPTCo.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
