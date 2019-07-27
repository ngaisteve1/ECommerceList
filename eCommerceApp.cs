using ConsoleTables;
using ecom;
using System;
using System.Collections.Generic;
using System.Linq;

public class eCommerce
{
    private const decimal deliverCharges = 10.00M;
    private List<Customer> validCustomers;

    public eCommerce()
    {
        validCustomers = new List<Customer>(){
            new Customer(){
                CustomerId = 1,
                Username = "john",
                Password = "john123",
                FullName = "John Wong",
                Email = "john@gmail.com",
                Address = "8, Jalan Emas, 58000, Kuala Lumpur."
                },
                new Customer(){
                CustomerId = 2,
                Username = "mike",
                Password = "mike123",
                FullName = "Mike Lee",
                Email = "mike@gmail.com",
                Address = "13, Jalan Api, 55100, Kuala Lumpur."
                }
        };
    }
    public void Execute()
    {
        var valid_Customer = Login();
        if (valid_Customer == null)
        {
            Console.WriteLine("Invalid username or password.");
            Console.ReadKey();
            return;
        }

        var shoppingCart = new List<OrderDetails>();
        var productList = new List<Product>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Azada E-Commerce App");
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

                    var table = new ConsoleTable("Product Id", "Product Name", "Unit Price");

                    foreach (var product in productList)
                        table.AddRow(product.Id, product.ProductName, product.UnitPrice);


                    table.Write();

                    Console.Write("Enter the product ID you want to buy: ");
                    int productId = int.Parse(Console.ReadLine());

                    var selectedProduct = productList.FirstOrDefault(p => p.Id == productId);

                    if (selectedProduct == null)
                    {
                        Console.WriteLine("Product not found.");
                        break;
                    }

                    Console.Write("Enter quantity to buy: ");
                    int quantity = int.Parse(Console.ReadLine());

                    var order = new OrderDetails();
                    order.Customer_Id = valid_Customer.CustomerId;
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
                    var totalOrderAmount = 0M;
                    totalOrderAmount = shoppingCart.Sum(s => s.TotalAmount);
                    var table2 = new ConsoleTable("Product Name", "Price", "Quantity", "Total");

                    if (shoppingCart2.ToList().Count == 0)
                    {
                        System.Console.WriteLine("Shopping cart is empty. Go to browse products.");
                        break;
                    }

                    foreach (var item in shoppingCart2)
                        table2.AddRow(item.ProductName, item.UnitPrice, item.QuantityOrder, item.TotalAmount);

                    table2.Write();

                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Delivery charges: " + deliverCharges);
                    Console.WriteLine("Total Order Amount: " + (totalOrderAmount + deliverCharges));
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Delivery Address: " + valid_Customer.Address);

                    Console.WriteLine("\n1. Confirm order and proceed to make payment.");
                    Console.WriteLine("2. Cancel order.");
                    Console.Write("Enter option:");
                    string opt2 = Console.ReadLine();
                    switch (opt2)
                    {
                        case "1":
                            Console.WriteLine("\nChoose payment method:");
                            Console.WriteLine("1. Cash on delivery (COD)");
                            Console.WriteLine("2. Credit Card");
                            Console.WriteLine("3. Online eBanking");
                            Console.ReadKey();
                            Console.WriteLine("Info: This E-Commerce simulation app ends here....");
                            break;
                        case "2":
                            shoppingCart.Clear();
                            System.Console.WriteLine("Shopping cart is empty. Go to browse products.");
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }

                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            Console.ReadKey();
        }
    }

    public Customer Login()
    {
        var userInput = new Customer();
        Console.Write("Username: ");
        userInput.Username = Console.ReadLine();

        Console.Write("Password: ");
        userInput.Password = Console.ReadLine();

        var validCustomer = validCustomers
        .Where(c => c.Username.Equals(userInput.Username))
        .Where(c => c.Password.Equals(userInput.Password))
        .FirstOrDefault();

        return validCustomer;
    }
}