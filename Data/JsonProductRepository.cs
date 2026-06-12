using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class JsonProductRepository
    {
        private readonly string _filePath = "products.json";

        private void SaveAll(List<Product> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Product> GetProducts()
        {
            if (!File.Exists(_filePath)) return new List<Product>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public void AddProduct(Product product)
        {
            var products = GetProducts();
            products.Add(product);
            SaveAll(products);
        }

        public void DeleteProduct(Guid id)
        {
            var products = GetProducts();
            products.RemoveAll(p => p.Id == id);
            SaveAll(products);
        }

        public void UpdateProduct(Product product)
        {
            var products = GetProducts();
            int index = products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                products[index] = product;
                SaveAll(products);
            }
        }
    }
}