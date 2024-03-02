using Microsoft.AspNetCore.Mvc;
using TrainingFPTCo.DataDBContext;
using TrainingFPTCo.Models;

namespace TrainingFPTCo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TrainingDbContext _dbContext;
        public CategoryController(TrainingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            CategoryDetail model = new CategoryDetail();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryDetail category, IFormFile PosterImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // upload file
                    string uniquePoster = UpLoadFile(PosterImage);
                    var categoryData = new Category()
                    {
                        Name = category.Name,
                        Description = category.Description,
                        PosterImage = uniquePoster,
                        ParentId = 0,
                        Status = category.Status,
                        CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };

                    // luu vao database
                    _dbContext.Categories.Add(categoryData);
                    _dbContext.SaveChangesAsync(true);
                    // thong bao trang thai
                    TempData["saveStatus"] = true; // luu thanh cong
                }
                catch (Exception ex)
                {
                    TempData["saveStatus"] = false; // luu that bai
                }
                // quay ve trang list categories
                return RedirectToAction(nameof(CategoryController.Index), "Category");
            }
            return View(category);
        }
        private string UpLoadFile(IFormFile file)
        {
            string uniqueFileName;
            try
            {
                string pathUploadServer = "wwwroot\\uploads\\images";
                string fileName = file.FileName;
                fileName = Path.GetFileName(fileName);
                // tao ten file khong trung nhau
                string uniqueStr = Guid.NewGuid().ToString(); // random string
                fileName = uniqueStr + "-" + fileName;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(),pathUploadServer, fileName);
                var stream = new FileStream(uploadPath, FileMode.Create);
                file.CopyToAsync(stream);
                uniqueFileName = fileName;
            }
            catch (Exception ex)
            {
                uniqueFileName = ex.Message.ToString();
            }
            return uniqueFileName;
        }
    }
}
