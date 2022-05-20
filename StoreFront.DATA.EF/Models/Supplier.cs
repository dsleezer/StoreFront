using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
