﻿namespace SUPERMARKET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //TEST ITEM

            Console.WriteLine("--------------------------------ITEMS------------------------------");
            Item item1 = new Item(1, "Manzana",true, 1.50, Category.FRUITS, Packaging.Unit, 50, 10);
            Item item2 = new Item(2, "Entrecot",false, 7.75, Category.MEAT, Packaging.Kg, 10, 20);

            //TEST PERSON 
            Console.WriteLine("--------------------------------PERSONS------------------------------");
            Person cashier1 = new Cashier("91827364A", "Ferran Chic", new DateTime(2015, 4, 13));
            Console.WriteLine(cashier1);

            Person customer1 = new Customer("98765432B", "Victor Granados", 73692827);
            Console.WriteLine(customer1);

            //TEST CASHIER
            Console.WriteLine("--------------------------------CASHIERS------------------------------");
            Cashier cashier2 = new Cashier("12345678A", "Artur Juve", new DateTime(2014, 03, 24));
            Console.WriteLine(cashier2);

            cashier2.AddPoints(150);
            Console.WriteLine($"+points: {cashier2}");

            //TEST CUSTOMER 
            Console.WriteLine("--------------------------------CUSTOMERS------------------------------");
            Customer customer2 = new Customer("98765432B", "Yaryna Blanco", 9872432);
            Console.WriteLine(customer2);

            customer2.AddPoints(50);
            Console.WriteLine($"+points: {customer2}");

            customer2.AddInvoiceAmount(100.50);
            Console.WriteLine($"+invoice: {customer2}");

            //TEST SUPERMARKET 
            Console.WriteLine("--------------------------------SUPERMARKET------------------------------");


            Supermarket supermarket = new Supermarket("BonPreu", "Calle Montilivi");

            supermarket.LoadCustomers("CUSTOMERS.TXT");
            supermarket.LoadCashiers("CASHIERS.TXT");
            supermarket.LoadWarehouse("GROCERIES.TXT");

            Console.WriteLine($"Nom: {supermarket.Name}");
            Console.WriteLine($"Adreça: {supermarket.Address}");
            Console.WriteLine($"Numero de linies obertes: {supermarket.ActiveLines}");

            supermarket.ActiveLines = 3;
            Console.WriteLine($"(+linies)Numero de linies obertes: {supermarket.ActiveLines}");



            //TEST ORDENACIO
            Console.WriteLine("--------------------------------ITEMS ORDENATS------------------------------");

            /*SortedSet<Item> itemsByStock = supermarket.GetItemByStock();
            itemsByStock.Add(new Item(1, "Peix",true, 10.50, Category.FROZEN, Packaging.Unit, 20, 5));
            itemsByStock.Add(new Item(2, "Platano",false,8.75, Category.FRUITS, Packaging.Unit, 15, 3));
            itemsByStock.Add(new Item(3, "Pan",true, 5.25, Category.BREAD, Packaging.Unit, 25, 10));
            itemsByStock.Add(new Item(4, "Pepino",false, 12.30, Category.VEGETABLES, Packaging.Unit, 18, 6));
         
            Console.WriteLine("Elementos ordenados por stock:");
            foreach (Item item in itemsByStock)
            {
               Console.WriteLine(item.ToString());
            }*/



        }
    }
}