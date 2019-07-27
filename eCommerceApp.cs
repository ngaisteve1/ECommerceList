using ConsoleTables;
using ecom;
using System;
using System.Collections.Generic;
using System.Linq;

public class eCommerce
{
    private const decimal deliverCharges = 10.00M;
    private List<Customer> validCustomers;
    private List<OrderDetails> shoppingCart;
    private List<Product> productList;

    public eCommerce()
    {
        InitializeCustomerList();
        InitializeProductList();
        shoppingCart = new List<OrderDetails>();
        Console.Title = "Azada e-Commerce App";
    }

    public void Execute()
    {
        var valid_Customer = Login();

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Login as {valid_Customer.FullName}");
            Console.WriteLine("\nAzada E-Commerce App");
            Console.WriteLine("1. Browse all products");
            Console.WriteLine("2. View shopping cart");
            Console.WriteLine("3. Logout");
            Console.Write("Enter your option: ");
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    BrowseProducts();
                    AddShoppingCart(valid_Customer);
                    break;
                case "2":
                    ViewShoppingCart(valid_Customer);
                    break;
                case "3":
                    Utility.printDotAnimation();
                    valid_Customer = null;
                    shoppingCart.Clear();
                    Execute();
                    break;
                default:
                    Utility.PrintMessage("Invalid option.", ConsoleColor.Red);
                    break;
            }
            Console.ReadKey();
        }
    }

    private Customer Login()
    {
        while (true)
        {
            var userInput = new Customer();
            Console.Clear();
            Console.Write("Username: ");
            userInput.Username = Console.ReadLine();

            Console.Write("Password: ");
            userInput.Password = Utility.ReadPassword();

            var validCustomer = validCustomers
            .Where(c => c.Username.Equals(userInput.Username))
            .Where(c => c.Password.Equals(userInput.Password))
            .FirstOrDefault();

            if (validCustomer != null)
            {
                Utility.printDotAnimation();
                return validCustomer;
            }


            Utility.PrintMessage("\nInvalid username or password.", ConsoleColor.Red);
            Console.ReadKey();
        }
    }

    private void InitializeCustomerList()
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

    private void InitializeProductList()
    {
        productList = new List<Product>() {
            new Product(){ Id = 2, ProductName = "MSI Laptop GL63 8RCS 058MY", UnitPrice = 2999.00M},
            new Product(){ Id = 5, ProductName = "MSI Laptop GL63 8RC 057MY", UnitPrice = 3799.00M},
            new Product(){ Id = 12, ProductName = "Logitech M330 Wireless Mouse", UnitPrice = 59.00M},
            new Product(){ Id = 13, ProductName = "Mechanical Keyboard with Backlight", UnitPrice = 160.00M},
            new Product(){ Id = 16, ProductName = "Dell Wireless Keyboard and Mouse Combo ", UnitPrice = 70.00M}
        };
    }

    private void BrowseProducts()
    {
        Console.Clear();

        var table = new ConsoleTable("Product Id", "Product Name", "Unit Price");

        foreach (var product in productList)
            table.AddRow(product.Id, product.ProductName, Utility.FormatAmount(product.UnitPrice));


        table.Write();
    }

    private void AddShoppingCart(Customer valid_Customer)
    {
        Console.Write("Enter the product ID you want to buy: ");
        int productId = int.Parse(Console.ReadLine());

        var selectedProduct = productList.FirstOrDefault(p => p.Id == productId);

        if (selectedProduct == null)
        {
            Utility.PrintMessage("Product not found.", ConsoleColor.Red);
            return;
        }

        Console.Write("Enter quantity to buy: ");
        int quantity = int.Parse(Console.ReadLine());

        var order = new OrderDetails();
        order.Customer_Id = valid_Customer.CustomerId;
        order.ProductId = productId;
        order.QuantityOrder = quantity;
        order.TotalAmount = quantity * selectedProduct.UnitPrice;

        shoppingCart.Add(order);
        Utility.PrintMessage($"{selectedProduct.ProductName} added into shopping cart.", ConsoleColor.Yellow);
    }

    private void ViewShoppingCart(Customer valid_Customer)
    {
        Console.Clear();

        // inner join orderdetail and product.
        var shoppingCart2 = from s in shoppingCart
                            join p in productList on s.ProductId equals p.Id
                            select new { p.ProductName, p.UnitPrice, s.QuantityOrder, s.TotalAmount };
        var totalOrderAmount = 0M;
        totalOrderAmount = shoppingCart.Sum(s => s.TotalAmount);
        var table = new ConsoleTable("Product Name", "Price", "Quantity", "Total");

        if (shoppingCart2.ToList().Count == 0)
        {
            Utility.PrintMessage("Shopping cart is empty. Go to browse products.", ConsoleColor.Yellow);
            return;
        }

        Console.WriteLine("Total Items in Shopping Cart: " + shoppingCart2.Count());
        foreach (var item in shoppingCart2)
            table.AddRow(item.ProductName, Utility.FormatAmount(item.UnitPrice), item.QuantityOrder, Utility.FormatAmount(item.TotalAmount));

        table.Options.EnableCount = false;
        table.Write();

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Delivery charges: " + Utility.FormatAmount(deliverCharges));
        Console.WriteLine("Total Order Amount: " + Utility.FormatAmount(totalOrderAmount + deliverCharges));
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Delivery Address: " + valid_Customer.Address);

        Console.WriteLine("\n1. Confirm order and proceed to make payment.");
        Console.WriteLine("2. Clear shopping cart.");
        Console.WriteLine("3. Back.");
        Console.Write("Enter option: ");
        string opt2 = Console.ReadLine();
        switch (opt2)
        {
            case "1":
                Console.WriteLine("\nChoose payment method:");
                Console.WriteLine("1. Cash on delivery (COD)");
                Console.WriteLine("2. Credit Card");
                Console.WriteLine("3. Online eBanking");
                Console.Write("Enter option: ");
                Console.ReadKey();
                Console.WriteLine("\n\nInfo: This E-Commerce simulation app ends here....");
                break;
            case "2":
                shoppingCart.Clear();
                Utility.PrintMessage("Shopping cart is empty. Go to browse products.", ConsoleColor.Yellow);
                break;
            case "3":
                break;
            default:
                Utility.PrintMessage("Invalid option.", ConsoleColor.Red);
                break;
        }

    }
}