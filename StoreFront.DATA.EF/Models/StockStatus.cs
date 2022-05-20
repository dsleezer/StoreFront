using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class StockStatus
    {
        public StockStatus()
        {
            Products = new HashSet<Product>();
        }

        public int StockStatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
