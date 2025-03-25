using THLTW_BT3.Models;
using THLTW_BT3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
namespace THLTW_BT3.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var category = await _categoryRepository.GetAllAsync();
            return View(category);
        }

        public async Task<IActionResult> Display(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin,Employee,Company")]
        public IActionResult Add()
        {
            return View(new Category());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                // Làm sạch dữ liệu: loại bỏ ký tự không mong muốn
                category.Name = category.Name?.Trim();
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    ModelState.AddModelError("Name", "Tên danh mục không được để trống.");
                    return View(category);
                }
                await _categoryRepository.AddAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Authorize(Roles = "Admin,Employee,Company")]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Làm sạch dữ liệu: loại bỏ ký tự không mong muốn
                category.Name = category.Name?.Trim();
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    ModelState.AddModelError("Name", "Tên danh mục không được để trống.");
                    return View(category);
                }
                _categoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Roles = "Admin,Employee,Company")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                _categoryRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
