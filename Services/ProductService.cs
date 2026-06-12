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
            List<Product> jsonList = jsonRepo.GetProducts();
            List<Product> dbList = dbRepo.GetProducts();

            Dictionary<Guid, Product> uniqueProducts = new Dictionary<Guid, Product>();

            foreach (var p in jsonList)
            {
                uniqueProducts[p.Id] = p;
            }


            foreach (var p in dbList)
            {
                uniqueProducts[p.Id] = p;
            }

            return new List<Product>(uniqueProducts.Values);
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