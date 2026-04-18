using System;
using System.Collections.Generic;
using Transaction.Models;
using Transaction.DataLogic;

namespace Transaction.BusinessLogic
{
    public class ProductService
    {
        private readonly JsonProductRepository jsonRepo = new JsonProductRepository();
        private readonly ProductDBRepository dbRepo = new ProductDBRepository();

        public void AddProduct(Product product)
        {
            jsonRepo.AddProduct(product);
            dbRepo.AddProduct(product);
        }

        public List<Product> GetProducts()
        {
            return jsonRepo.GetProducts();
        }

        public void DeleteProduct(Guid id)
        {
            jsonRepo.DeleteProduct(id);
            dbRepo.DeleteProduct(id);
        }

        public void UpdateProduct(Product product)
        {
            jsonRepo.UpdateProduct(product);
            dbRepo.UpdateProduct(product);
        }
    }
}
