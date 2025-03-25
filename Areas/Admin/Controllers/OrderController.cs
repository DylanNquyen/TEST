using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using THLTW_BT3.Models;
using THLTW_BT3.Repositories;

namespace THLTW_BT3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index(string customerId = null, string status = null, DateTime? orderDate = null)
{
    var orders = await _orderRepository.GetAllAsync();

    // Lọc đơn hàng nếu có tham số tìm kiếm
    if (!string.IsNullOrEmpty(customerId))
    {
        orders = orders.Where(o => o.CustomerId.Contains(customerId, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrEmpty(status))
    {
        orders = orders.Where(o => o.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase));

    }

    if (orderDate.HasValue)
    {
        orders = orders.Where(o => o.OrderDate.Date == orderDate.Value.Date);
    }

    // Lưu các tham số tìm kiếm vào ViewBag để hiển thị lại trong form
    ViewBag.CustomerId = customerId;
    ViewBag.Status = status;
    ViewBag.OrderDate = orderDate?.ToString("yyyy-MM-dd");
            return View(orders);
        }

        public async Task<IActionResult> Add()
        {
            // Lấy danh sách sản phẩm để hiển thị trong form
            var products = await _productRepository.GetAllAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order order)
        {
            if (ModelState.IsValid)
            {
                // Tính tổng tiền từ OrderDetails
                order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
                await _orderRepository.AddAsync(order);
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, trả lại form với dữ liệu đã nhập
            var products = await _productRepository.GetAllAsync();
            ViewBag.Products = new SelectList(products, "Id", "Name");
            return View(order);
        }

        public async Task<IActionResult> Display(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _orderRepository.UpdateAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}