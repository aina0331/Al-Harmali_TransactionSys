using System;

namespace Transaction.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Company { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public int Stock { get; set; }
    }
}