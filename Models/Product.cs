using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace THLTW_BT3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(0.01, 10000.00, ErrorMessage = "Giá sản phẩm phải từ 0.01 đến 10000.00")]
        [Display(Name = "Giá")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh chính")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Danh sách hình ảnh")]
        public List<ProductImage>? Images { get; set; }

        [Required(ErrorMessage = "Danh mục không được để trống")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Danh mục")]
        public Category? Category { get; set; }

        [Display(Name = "Số lượng trong kho")]
        public int StockQuantity { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Đánh giá trung bình")]
        public double AverageRating { get; set; }

        [Display(Name = "Số lượt đánh giá")]
        public int RatingCount { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "URL hình ảnh không được để trống")]
        [Display(Name = "URL hình ảnh")]
        public string Url { get; set; }

        [Display(Name = "Mô tả hình ảnh")]
        public string? AltText { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int DisplayOrder { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Sản phẩm")]
        public Product? Product { get; set; }
    }
}
