namespace SUPERMARKET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //TEST ITEM

            Item item1 = new Item(1, "Manzana", 1.50, Item.Category.FRUITS, Item.Packaging.Unit, 50, 10);
            Item item2 = new Item(2, "Entrecot", 7.75, Item.Category.MEAT, Item.Packaging.Kg, 100, 20);

            Console.WriteLine("--------------------------------ITEMS------------------------------");
            Console.WriteLine(item1);
            Console.WriteLine(item2);

            //TEST PERSON 
            Console.WriteLine("--------------------------------PERSONS------------------------------");
            Person cashier1 = new Cashier ("91827364A", "Ferran Chic", 100, new DateTime(2015,4,13));
            Console.WriteLine(cashier1);
            
            Person customer1 = new Customer ("98765432B", "Victor Granados", 73692827);
            Console.WriteLine(customer1);

            //TEST CASHIER
            Console.WriteLine("--------------------------------CASHIERS------------------------------");
            Cashier cashier2 = new Cashier("12345678A", "Artur Juve", 100, new DateTime(2014,03,24));
            Console.WriteLine(cashier2);

            cashier2.AddPoints(50);
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

            // Crear un supermercado
            Supermarket supermarket = new Supermarket("BonPreu", "Calle Montilivi");

            // Cargar los datos de los archivos
            supermarket.LoadCustomers("CUSTOMERS.TXT");
            supermarket.LoadCashiers("CASHIERS.TXT");
            supermarket.LoadWarehouse("GROCERIES.TXT");

            // Mostrar información del supermercado
            Console.WriteLine("Supermarket information:");
            Console.WriteLine($"Name: {supermarket.Name}");
            Console.WriteLine($"Address: {supermarket.Address}");
            Console.WriteLine($"Number of active lines: {supermarket.ActiveLines}");

            // Puedes acceder a los diccionarios cargados en el supermercado y hacer operaciones adicionales si es necesario
            // Por ejemplo, podrías imprimir los nombres de los clientes cargados:
            Console.WriteLine("\nCustomers:");
            foreach (var customer in supermarket.Customers)
            {
                Console.WriteLine($"ID: {customer.Key}, Name: {customer.Value}");
            }
        }
    }
}