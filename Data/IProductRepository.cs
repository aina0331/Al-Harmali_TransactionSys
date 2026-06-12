using System;
using System.Collections.Generic;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);
    }
}