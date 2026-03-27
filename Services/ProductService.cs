using System;
using System.Collections.Generic;
using Transaction.Models;
using Transaction.DataLogic;

namespace Transaction.BusinessLogic
{
    public class ProductService
    {
        private readonly JsonProductRepository _jsonRepo;
        private readonly ProductDBRepository _dbRepo;

        public ProductService()
        {
            _jsonRepo = new JsonProductRepository();
            _dbRepo = new ProductDBRepository();
        }

        public void AddProduct(Product p)
        {
            _jsonRepo.AddProduct(p);
            _dbRepo.AddProduct(p);
        }

        public void DeleteProduct(Guid id)
        {
            _jsonRepo.DeleteProduct(id);
            _dbRepo.DeleteProduct(id);
        }

        public List<Product> GetProducts()
        {
            return _jsonRepo.GetProducts(); 
        }
    }
}