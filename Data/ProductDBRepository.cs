using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class ProductDBRepository : IProductRepository
    {
        private readonly string _connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=TransactionsDB;Integrated Security=True;TrustServerCertificate=True;";

        public void AddProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO Products (Id, Company, Item, PurchasePrice, SellingPrice, Stock) VALUES (@Id, @Company, @Item, @PurchasePrice, @SellingPrice, @Stock)", conn);

            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Company", product.Company);
            cmd.Parameters.AddWithValue("@Item", product.Item);
            cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
            cmd.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.ExecuteNonQuery();
        }

        public List<Product> GetProducts()
        {
            var list = new List<Product>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Company, Item, PurchasePrice, SellingPrice, Stock FROM Products", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Product
                {
                    Id = reader.GetGuid(0),
                    Company = reader.GetString(1),
                    Item = reader.GetString(2),
                    PurchasePrice = reader.GetDouble(3),
                    SellingPrice = reader.GetDouble(4),
                    Stock = reader.GetInt32(5)
                });
            }
            return list;
        }

        public void DeleteProduct(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Products WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                "UPDATE Products SET Company=@Company, Item=@Item, PurchasePrice=@PurchasePrice, SellingPrice=@SellingPrice, Stock=@Stock WHERE Id=@Id", conn);

            cmd.Parameters.AddWithValue("@Company", product.Company);
            cmd.Parameters.AddWithValue("@Item", product.Item);
            cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
            cmd.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.ExecuteNonQuery();
        }
    }
}