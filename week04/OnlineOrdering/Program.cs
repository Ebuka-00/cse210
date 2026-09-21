using System;

class Program
{
    static void Main(string[] args)
    {
        // Order 1 - Customer in the USA
        Address address1 = new Address(
            "123 Main Street",
            "Salt Lake City",
            "Utah",
            "USA"
        );

        Customer customer1 = new Customer(
            "John Smith",
            address1
        );

        Product product1 = new Product(
            "Laptop",
            "P001",
            800,
            1
        );

        Product product2 = new Product(
            "Wireless Mouse",
            "P002",
            25,
            2
        );

        Product product3 = new Product(
            "Keyboard",
            "P003",
            50,
            1
        );

        Order order1 = new Order(customer1);

        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);


        // Order 2 - Customer outside the USA
        Address address2 = new Address(
            "15 Okigwe Road",
            "Aba",
            "Abia",
            "Nigeria"
        );

        Customer customer2 = new Customer(
            "Ebuka Okubalu",
            address2
        );

        Product product4 = new Product(
            "Headphones",
            "P004",
            60,
            2
        );

        Product product5 = new Product(
            "Phone Charger",
            "P005",
            20,
            3
        );

        Order order2 = new Order(customer2);

        order2.AddProduct(product4);
        order2.AddProduct(product5);


        // Display Order 1
        Console.WriteLine("========================================");
        Console.WriteLine("ORDER 1");
        Console.WriteLine("========================================");

        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}");

        Console.WriteLine();


        // Display Order 2
        Console.WriteLine("========================================");
        Console.WriteLine("ORDER 2");
        Console.WriteLine("========================================");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}");
    }
}