using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class ProductDBRepository
    {
        private readonly string _connectionString =
            "Data Source=AINAGELS\\SQLEXPRESS;Initial Catalog=TransactionsDB;Integrated Security=True;TrustServerCertificate=True;";

        public void AddProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Products (Id, Company, Item, PurchasePrice, SellingPrice, Stock) VALUES (@Id,@Company,@Item,@PurchasePrice,@SellingPrice,@Stock)",
                conn);

            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Company", product.Company);
            cmd.Parameters.AddWithValue("@Item", product.Item);
            cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
            cmd.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            cmd.Parameters.AddWithValue("@Stock", product.Stock);
            cmd.ExecuteNonQuery();
        }

        public void DeleteProduct(Guid id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Products WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateProduct(Product product)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(
                "UPDATE Products SET Company=@Company, Item=@Item, PurchasePrice=@PurchasePrice, SellingPrice=@SellingPrice, Stock=@Stock WHERE Id=@Id",
                conn);

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
