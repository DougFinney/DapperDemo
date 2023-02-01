using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DapperDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);

            //Departments Section
            
            //var departments = repo.GetAllDepartments();
            
            //foreach (var dept in departments)
            //{
            // Console.WriteLine(dept.Name);
            //}

            //Console.WriteLine("Please enter a new Department name");

            //var newDepartment = Console.ReadLine();

            //repo.InsertDepartment(newDepartment);

            //Get updated Depts list
            //var departments = repo.GetAllDepartments();
            ////print Depts list with updated name included
            //foreach(var dept in departments) 
            //{
            //    Console.WriteLine(dept.Name);
            //}

            //PRODUCTS SECTION
            var prodRepo = new DapperProductRepository(conn);

            var products = prodRepo.GetAllProducts();

            foreach (var prod in products) 
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }

            //CREATE PRODUCT
            Console.WriteLine("What is the new product name?");
            var prodName = Console.ReadLine();

            Console.WriteLine("What is the price?");
            var prodPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the CategoryID?");
            var prodCat = int.Parse(Console.ReadLine());

            prodRepo.CreateProduct(prodName, prodPrice, prodCat);
           
            products = prodRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }


            //UPDATED PRODUCT
            Console.WriteLine("Enter a ProductID to update");
            var prodID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the updated name");
            prodName = Console.ReadLine();

            prodRepo.UpdateProduct(prodID, prodName);

            products = prodRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }

            //DELETE
            Console.WriteLine("What is the ProductID you will delete?");
            prodID = int.Parse(Console.ReadLine());

            prodRepo.DeleteProduct(prodID);

            products = prodRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
                
              
            }


        }
    }
}