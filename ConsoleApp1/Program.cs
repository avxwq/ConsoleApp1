using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        ProductTable table = new ProductTable();
        Product product = new Product(0, "Kapusta", "Spożywcze", 20, 1, "Ok");
        Product product1 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product2 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product3 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product4 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product5 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product6 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product7 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        Product product8 = new Product(1, "Róża", "Ogrodowe", 50, 25, "Róża do dekoracji");
        table.AddProduct(product);
        table.AddProduct(product2);
        table.AddProduct(product3);
        table.AddProduct(product4);
        table.AddProduct(product5);
        table.AddProduct(product6);

        View view = new View(table);

        while (true) { }
    }
}

