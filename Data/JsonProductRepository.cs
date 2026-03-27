using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Transaction.Models;
using System.Collections.Generic;

namespace Transaction.DataLogic
{
    public class JsonProductRepository
    {
        private readonly string _filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/Products.json";
        private List<Product> _products;

        public JsonProductRepository()
        {
            _products = new List<Product>();
            LoadFromJson();
        }

        private void LoadFromJson()
        {
            if (!File.Exists(_filePath))
            {
                SaveToJson(); // create empty file
                return;
            }

            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<List<Product>>(json);
            _products = data ?? new List<Product>();
        }

        private void SaveToJson()
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void AddProduct(Product p)
        {
            _products.Add(p);
            SaveToJson();
        }

        public void DeleteProduct(Guid id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                SaveToJson();
            }
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}