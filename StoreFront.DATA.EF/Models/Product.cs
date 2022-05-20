using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Product
    {
        public Product()
        {
            Games = new HashSet<Game>();
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public bool IsActive { get; set; }
        public int SupplierId { get; set; }
        public string? PhotoUrl { get; set; }
        public int StockStatusId { get; set; }
        public int ProductTypeId { get; set; }

        public virtual ProductType? ProductType { get; set; }
        public virtual StockStatus? StockStatus { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
