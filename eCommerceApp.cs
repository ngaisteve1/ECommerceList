using ConsoleTables;
using ecom;
using System;
using System.Collections.Generic;
using System.Linq;

public class eCommerce
{
    private static decimal deliverCharges = 10;
    public void Execute()
    {
        var shoppingCart = new List<OrderDetails>();
        var productList = new List<Product>();
        while (true)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Browser all products.");
            Console.WriteLine("2. View shopping cart.");
            Console.Write("Enter your option: ");
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    productList = new List<Product>() {
                        new Product(){ Id = 2, ProductName = "MSI Laptop GL63 8RCS 058MY", UnitPrice = 2999.00M},
                        new Product(){ Id = 5, ProductName = "MSI Laptop GL63 8RC 057MY", UnitPrice = 3799.00M},
                        new Product(){ Id = 12, ProductName = "Logitech M330 Wireless Mouse", UnitPrice = 59.00M},
                        new Product(){ Id = 13, ProductName = "Mechanical Keyboard with Backlight", UnitPrice = 160.00M},
                        new Product(){ Id = 16, ProductName = "Dell Wireless Keyboard and Mouse Combo ", UnitPrice = 70.00M}
                    };

                    var table = new ConsoleTable("Id", "Product Name", "Unit Price");

                    foreach (var product in productList)
                        table.AddRow(product.Id, product.ProductName, product.UnitPrice);


                    table.Write();

                    Console.WriteLine("Enter the product ID you want to buy: ");
                    int productId = int.Parse(Console.ReadLine());

                    var selectedProduct = productList.FirstOrDefault(p => p.Id == productId);

                    if (selectedProduct == null)
                    {
                        Console.WriteLine("Product not found.");
                        return;
                    }

                    Console.WriteLine("Enter quantity to buy: ");
                    int quantity = int.Parse(Console.ReadLine());

                    var order = new OrderDetails();
                    order.ProductId = productId;
                    order.QuantityOrder = quantity;
                    order.TotalAmount = quantity * selectedProduct.UnitPrice;

                    shoppingCart.Add(order);
                    Console.WriteLine($"{selectedProduct.ProductName} added into shopping cart.");

                    break;
                case "2":
                    // inner join orderdetail and product.
                    var shoppingCart2 = from s in shoppingCart
                                        join p in productList on s.ProductId equals p.Id
                                        select new { p.ProductName, p.UnitPrice, s.QuantityOrder, s.TotalAmount };
                    var totalOrderAmount = shoppingCart.Sum(s => s.TotalAmount);
                    var table2 = new ConsoleTable("Product Name", "Price", "Quantity", "Total");

                    foreach (var item in shoppingCart2)
                        table2.AddRow(item.ProductName, item.UnitPrice, item.QuantityOrder, item.TotalAmount);

                    table2.Write();

                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Delivery charges: " + deliverCharges);
                    Console.WriteLine("Total Order Amount: " + totalOrderAmount + deliverCharges);
                    break;
                default:
                    break;
            }
        }
    }
}