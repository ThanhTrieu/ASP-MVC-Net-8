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

        [HttpPost]
        public IActionResult Logout()
        {
            // xoa session da tao ra o login
            // quay ve trang dang nhap
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserId")))
            {
                // xoa session
                HttpContext.Session.Remove("SessionUserId");
                HttpContext.Session.Remove("SessionUsername");
                HttpContext.Session.Remove("SessionRolesId");
                HttpContext.Session.Remove("SessionEmail");
            }
            // quay ve trang dang nhap
            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
