using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace THLTW_BT3.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Mã người dùng")]
        public string? UserId { get; set; }

        [Display(Name = "Danh sách sản phẩm")]
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Tổng tiền")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount => Items.Sum(item => item.Subtotal);

        [Display(Name = "Số lượng sản phẩm")]
        public int TotalItems => Items.Sum(item => item.Quantity);

        public void AddItem(CartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                item.CreatedAt = DateTime.Now;
                item.UpdatedAt = DateTime.Now;
                Items.Add(item);
            }
            UpdatedAt = DateTime.Now;
        }

        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.ProductId == productId);
            UpdatedAt = DateTime.Now;
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                item.UpdatedAt = DateTime.Now;
                UpdatedAt = DateTime.Now;
            }
        }

        public void Clear()
        {
            Items.Clear();
            UpdatedAt = DateTime.Now;
        }

        public bool HasItems => Items.Any();
    }
}
