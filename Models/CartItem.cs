using System.ComponentModel.DataAnnotations;

namespace THLTW_BT3.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
