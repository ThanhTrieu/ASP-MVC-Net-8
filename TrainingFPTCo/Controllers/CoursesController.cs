using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class CoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> itemCategories = new List<SelectListItem>();
            var dataCategory = new CategoryQuery().GetAllCategories(null, null);
            foreach (var item in dataCategory)
            {
                itemCategories.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            
            ViewBag.Categories = itemCategories;
            return View();
        }
    }
}
