using Microsoft.AspNetCore.Mvc;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        } 

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            model = new LoginQuery().CheckUserLogin(model.UserName, model.Password);
            if (string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.Email))
            {
                // nguoi dung dang nhap linh tinh
                // co mot thong bao loi ra view
                ViewData["MessageLogin"] = "Account invalid";
                return View(model);
            }

            // luu thong tin nguoi dung vao session
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserId")))
            {
                HttpContext.Session.SetString("SessionUserId", model.Id);
                HttpContext.Session.SetString("SessionUsername", model.UserName);
                HttpContext.Session.SetString("SessionRolesId", model.RolesId);
                HttpContext.Session.SetString("SessionEmail", model.Email);
            }

            // cho chuyen vao trang Home
            return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
        }
    }
}
