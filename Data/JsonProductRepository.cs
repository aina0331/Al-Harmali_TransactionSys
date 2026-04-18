using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class JsonProductRepository
    {
        private readonly string filePath = "products.json";

        public List<Product> GetProducts()
        {
            if (!File.Exists(filePath)) return new List<Product>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        private void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void AddProduct(Product product)
        {
            var products = GetProducts();
            products.Add(product);
            SaveProducts(products);
        }

        public void DeleteProduct(Guid id)
        {
            var products = GetProducts();
            products.RemoveAll(p => p.Id == id);
            SaveProducts(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = GetProducts();
            var index = products.FindIndex(p => p.Id == product.Id);
            if (index >= 0)
            {
                products[index] = product;
                SaveProducts(products);
            }
        }
    }
}
