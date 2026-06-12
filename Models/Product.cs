using System;
using System.Globalization;

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

        public string GetName() => Item;
        public string GetCompany() => Company;

        public void AddStock(int amount) => Stock += amount;

        public void RemoveStock(int amount)
        {
            Stock -= amount;
            if (Stock < 0) Stock = 0;
        }

        public string GetDetails()
        {
   
            var phCulture = new CultureInfo("en-PH");
            string buyPrice = PurchasePrice.ToString("C2", phCulture);
            string sellPrice = SellingPrice.ToString("C2", phCulture);

            return $"{Company} - {Item}, Stock: {Stock}, Buy: {buyPrice}, Sell: {sellPrice}";
        }
    }
}