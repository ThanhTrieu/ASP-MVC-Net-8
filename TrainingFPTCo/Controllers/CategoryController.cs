using Microsoft.AspNetCore.Mvc;
using TrainingFPTCo.DataDBContext;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;
using TrainingFPTCo.Helpers;

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
            CategoryViewModel categoryModel  = new CategoryViewModel();
            categoryModel.CategoryDetailList = new List<CategoryDetail>();
            var dataCategory = new CategoryQuery().GetAllCategories();
            foreach (var item in dataCategory)
            {
                categoryModel.CategoryDetailList.Add(new CategoryDetail
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    PosterNameImage = item.PosterNameImage,
                    Status = item.Status,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                });
            }
            return View(categoryModel);
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
                    /*
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
                    */
                    int idCategory = new CategoryQuery().InsertItemCategory(category.Name, category.Description, uniquePoster, category.Status);
                    if (idCategory > 0)
                    {
                        // thong bao trang thai
                        TempData["saveStatus"] = true; // luu thanh cong
                    }
                    else
                    {
                        // thong bao trang thai
                        TempData["saveStatus"] = false; // luu that bai
                    }
                    
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
                
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                fileNameWithoutExtension = CommonText.GenerateSlug(fileNameWithoutExtension);

                string ext = Path.GetExtension(fileName);

                // tao ten file khong trung nhau
                string uniqueStr = Guid.NewGuid().ToString(); // random string
                string fileNameUpload = uniqueStr + "-" + fileNameWithoutExtension + ext;

                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(),pathUploadServer, fileNameUpload);
                var stream = new FileStream(uploadPath, FileMode.Create);
                file.CopyToAsync(stream);
                uniqueFileName = fileNameUpload;
            }
            catch (Exception ex)
            {
                uniqueFileName = ex.Message.ToString();
            }
            return uniqueFileName;
        }
    }
}
