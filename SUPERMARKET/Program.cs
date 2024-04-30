namespace SUPERMARKET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // CREAR CLIENTS
            Customer customer1 = new Customer("123456789A", "Yaryna Blanco", 1000);
            customer1.AddInvoiceAmount(500); 
            customer1.Active = true; 
           
            Customer customer2 = new Customer("987654321B", "Victor Granados",200);
            customer2.AddInvoiceAmount(700); 
            customer2.Active = false;
                   
            Console.WriteLine(customer1.ToString());         
            Console.WriteLine(customer2.ToString());

        }
    }
}