using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class ProductDBRepository
    {
        private readonly string _connectionString =
            "Data Source=Aina0331\\SQLEXPRESS;Initial Catalog=TransactionDB;Integrated Security=True;TrustServerCertificate=True;";

        public void AddProduct(Product p)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO Products (Id, Company, Item, PurchasePrice, SellingPrice, Stock) " +
                "VALUES (@Id, @Company, @Item, @PurchasePrice, @SellingPrice, @Stock)", conn);

            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Company", p.Company);
            cmd.Parameters.AddWithValue("@Item", p.Item);
            cmd.Parameters.AddWithValue("@PurchasePrice", p.PurchasePrice);
            cmd.Parameters.AddWithValue("@SellingPrice", p.SellingPrice);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);

            cmd.ExecuteNonQuery();
        }

        public void DeleteProduct(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Products WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
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
    }
}