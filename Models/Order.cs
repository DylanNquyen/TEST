using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace THLTW_BT3.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Chờ xử lý")]
        Pending,
        [Display(Name = "Đã xác nhận")]
        Confirmed,
        [Display(Name = "Đang xử lý")]
        Processing,
        [Display(Name = "Đang vận chuyển")]
        Shipped,
        [Display(Name = "Đã giao hàng")]
        Delivered,
        [Display(Name = "Đã hủy")]
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        [Display(Name = "Mã đơn hàng")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Khách hàng không được để trống")]
        [Display(Name = "Khách hàng")]
        public string CustomerId { get; set; }

        [Display(Name = "Thông tin khách hàng")]
        public ApplicationUser Customer { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng không được để trống")]
        [Display(Name = "Ngày đặt hàng")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Tổng tiền không được để trống")]
        [Display(Name = "Tổng tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Trạng thái đơn hàng không được để trống")]
        [Display(Name = "Trạng thái")]
        public OrderStatus Status { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public bool IsPaid { get; set; }

        [Display(Name = "Ngày thanh toán")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Chi tiết đơn hàng")]
        public List<OrderDetail> OrderDetails { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class OrderDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng không được để trống")]
        [Display(Name = "Mã đơn hàng")]
        public int OrderId { get; set; }

        [Display(Name = "Đơn hàng")]
        public Order Order { get; set; }

        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        [Display(Name = "Sản phẩm")]
        public int ProductId { get; set; }

        [Display(Name = "Thông tin sản phẩm")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Đơn giá không được để trống")]
        [Display(Name = "Đơn giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Tổng tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal => Quantity * UnitPrice;
    }
}
