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
        public IActionResult Index(string SearchString)
        {
            CategoryViewModel categoryModel  = new CategoryViewModel();
            categoryModel.CategoryDetailList = new List<CategoryDetail>();
            var dataCategory = new CategoryQuery().GetAllCategories(SearchString);
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
                    string uniquePoster = UploadFileHelper.UpLoadFile(PosterImage);
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

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            CategoryDetail categoryDetail = new CategoryQuery().GetDataCategoryById(id);
            return View(categoryDetail);
        }

        [HttpPost]
        public IActionResult Update(CategoryDetail category, IFormFile PosterImage)
        {

            try
            {
                var detail = new CategoryQuery().GetDataCategoryById(category.Id);
                string uniqueFilePoster = detail.PosterNameImage;
                // nguoi dung co upload file ko?
                if (category.PosterImage != null)
                {
                    // co upload de thay anh poster
                    uniqueFilePoster = UploadFileHelper.UpLoadFile(PosterImage);
                }
                bool updateCategory = new CategoryQuery().UpdateCategoryById(category.Name, category.Description, uniqueFilePoster, category.Status, category.Id);
                if (updateCategory)
                {
                    TempData["updateStatus"] = true;
                }
                else
                {
                    TempData["updateStatus"] = false;
                }
                return RedirectToAction(nameof(CategoryController.Index), "Category");
            }
            catch (Exception ex)
            {
                //return Ok(ex.Message);
                return View(category);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            // xu ly xoa danh muc khoa hoc
            bool deleteCategory = new CategoryQuery().DeleteItemCatgoryById(id);
            if (deleteCategory)
            {
                TempData["statusDelete"] = true;
            }
            else
            {
                TempData["statusDelete"] = false;
            }
            return RedirectToAction(nameof(CategoryController.Index), "Category");
        }
    }
}
