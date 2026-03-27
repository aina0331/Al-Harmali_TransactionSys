using System;
using Transaction.Models;

namespace Transaction.DataLogic
{
    public class InMemoryProductRepository
    {
        public Product[] products = new Product[100];
        public int count = 0;

        public void AddProduct(Product p)
        {
            if (count < products.Length)
            {
                products[count] = p;
                count++;
            }
        }

        public void DeleteProduct(int index)
        {
            if (index < 0 || index >= count) return;

            for (int i = index; i < count - 1; i++)
            {
                products[i] = products[i + 1];
            }
            products[count - 1] = null;
            count--;
        }
    }
}