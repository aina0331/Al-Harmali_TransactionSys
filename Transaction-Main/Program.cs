using System;
using Transaction.Models;
using Transaction.BusinessLogic;

namespace Transaction
{
    internal class Program
    {
        static ProductService service = new ProductService();

        static void Main(string[] args)
        {
            Console.WriteLine("Company Transactions from Vendors.");

            while (true)
            {
                Console.WriteLine("\n1 = Add Item");
                Console.WriteLine("2 = View Stocks");
                Console.WriteLine("3 = Delete Item");
                Console.WriteLine("4 = Update Item");
                Console.WriteLine("5 = Exit");

                Console.Write("\nPlease Input Your Choice: ");
                int choice = ReadInt();

                if (choice == 1)
                {
                    Product p = new Product();

                    Console.Write("Company Name: ");
                    p.Company = Console.ReadLine() ?? "";

                    Console.Write("Item Name: ");
                    p.Item = Console.ReadLine() ?? "";

                    Console.Write("Purchase Price: ");
                    p.PurchasePrice = ReadDouble();

                    Console.Write("Selling Price: ");
                    p.SellingPrice = ReadDouble();

                    Console.Write("Stock: ");
                    p.Stock = ReadInt();

                    service.AddProduct(p);
                    Console.WriteLine("Item Added Successfully!");
                }
                else if (choice == 2)
                {
                    var products = service.GetProducts();
                    int i = 1;
                    foreach (var p in products)
                    {
                        Console.WriteLine($"\nItem Number: {i}");
                        Console.WriteLine("Name: " + p.GetName());
                        Console.WriteLine("Company: " + p.GetCompany());
                        Console.WriteLine("Details: " + p.GetDetails());
                        i++;
                    }
                }
                else if (choice == 3)
                {
                    Console.Write("Enter item number to delete: ");
                    int index = ReadInt() - 1;
                    var products = service.GetProducts();
                    if (index >= 0 && index < products.Count)
                    {
                        service.DeleteProduct(products[index].Id);
                        Console.WriteLine("Item Deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid item number.");
                    }
                }
                else if (choice == 4)
                {
                    var products = service.GetProducts();
                    int i = 1;
                    foreach (var p in products)
                    {
                        Console.WriteLine($"\nItem Number: {i}");
                        Console.WriteLine("Details: " + p.GetDetails());
                        i++;
                    }

                    Console.WriteLine("Enter item number to update:");
                    int index = ReadInt() - 1;
                    if (index >= 0 && index < products.Count)
                    {
                        Product product = products[index];

                        Console.Write("New Company (leave blank to keep): ");
                        string company = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(company)) product.Company = company;

                        Console.Write("New Item (leave blank to keep): ");
                        string item = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(item)) product.Item = item;

                        Console.Write("New Purchase Price (0 to keep): ");
                        double purchase = ReadDouble();
                        if (purchase > 0) product.PurchasePrice = purchase;

                        Console.Write("New Selling Price (0 to keep): ");
                        double selling = ReadDouble();
                        if (selling > 0) product.SellingPrice = selling;

                        Console.Write("New Stock (0 to keep): ");
                        int stock = ReadInt();
                        if (stock > 0) product.Stock = stock;

                        service.UpdateProduct(product);
                        Console.WriteLine("Item Updated!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid item number.");
                    }
                }
                else if (choice == 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }

        static int ReadInt()
        {
            int v;
            while (!int.TryParse(Console.ReadLine(), out v))
                Console.Write("Enter a valid integer: ");
            return v;
        }

        static double ReadDouble()
        {
            double v;
            while (!double.TryParse(Console.ReadLine(), out v))
                Console.Write("Enter a valid number: ");
            return v;
        }
    }
}
