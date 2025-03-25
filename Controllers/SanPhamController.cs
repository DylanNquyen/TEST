using Microsoft.AspNetCore.Mvc;
using THLTW_BT3.Repositories;

namespace THLTW_BT3.Controllers
{
    public class SanPhamController : Controller
    {
        //Mục đích controller này để hiển thị layout sản phẩm cho thật đẹp
        //Và user bấm add to cart
        private readonly IProductRepository _productRepository;

        public SanPhamController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
    }
}
