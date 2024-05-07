using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Supermarket
    {

        #region ATRIBUTS
        private string name;
        private string address;
        public static int MAXLINES = 5;
        private int activeLines;
        private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        private Dictionary<Item, double> ShoppingCart;
        Dictionary<string, Person> staff;
        Dictionary<string, Person> customers;
        SortedDictionary<int, Item> warehouse;

        #endregion

        #region CONSTRUCTORS
        public class CheckOutLine
        {
            private int number;
            //private Queue<ShoppingCart> queue;
            private Person cashier;
            private bool active;
        }

        public Supermarket(string name, string address)
        {
            this.name = name;
            this.address = address;
            activeLines = 1;
            lines = new CheckOutLine[MAXLINES];
            ShoppingCart = new Dictionary<Item, double>();
        }

        public Supermarket(string name, string address, string fileCustomers, string fileItems, int activeLines) : this(name, address)
        {
            LoadCustomers("CUSTOMERS.TXT");
            LoadCashiers("CASHIERS.TXT");
            LoadWarehouse("GROCERIES.TXT");
            this.activeLines = activeLines;
        }
        #endregion

        #region PROPIETATS
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int ActiveLines
        {
            get { return activeLines; }
            set
            {
                if (value >= 1 && value <= MAXLINES)
                {
                    activeLines = value;
                }
                else
                {
                    throw new ArgumentException("Active lines must be between 1 and MAXLINES.");
                }
            }
        }
        #endregion

        // Method to initialize checkout lines

        public void InitializeCheckOutLines()
        {
            for (int i = 0; i < MAXLINES; i++)
            {
                lines[i] = new CheckOutLine(); 
            }
        }

        //METODOS PRIVADOS

        #region LECTURA_FITXERS

        private Dictionary<string, string> LoadCustomers(string fileName)
        {
            Dictionary<string, string> customers = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(fileName);

            string line;
           
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 2)
                {
                    customers.Add(parts[0], parts[1]);
                }
            }

            return customers;
        }
        public void LoadCustomersP(string fileName)
        {
            LoadCustomers(fileName);
        }


        private Dictionary<string, string> LoadCashiers(string fileName)
        {
            Dictionary<string, string> cashiers = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(fileName);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 2)
                {
                    cashiers.Add(parts[0], parts[1]);
                }
            }

            return cashiers;
        }
        public void LoadCashiersP(string fileName)
        {
            LoadCashiers(fileName);
        }


        private Dictionary<string, double> LoadWarehouse(string fileName)
        {
            Dictionary<string, double> products = new Dictionary<string, double>();
            StreamReader sr = new StreamReader(fileName);
            
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                    
                if (parts.Length >= 5)
                {
                        
                    products.Add(parts[0], Convert.ToDouble(parts[4]));
                }
                    
            }
            
            return products;
        }
        public void LoadWarehouseP(string fileName)
        {
            LoadWarehouse(fileName);
        }
        #endregion

        private Item.Packaging TranslateToPackaging(char packagingChar)
        {
            switch (packagingChar)
            {
                case 'K':
                    return Item.Packaging.Kg;
                case 'U':
                    return Item.Packaging.Unit;
                case 'P':
                    return Item.Packaging.Package;
                default:
                    throw new ArgumentException("Invalid packaging character.");
            }

        }

        public SortedSet<Item> GetItemByStock()
        {
            Comparer<Item> stockComparer = Comparer<Item>.Create((item1, item2) => item1.Stock.CompareTo(item2.Stock));

            SortedSet<Item> itemsByStock = new SortedSet<Item>(stockComparer);

            foreach (KeyValuePair<string, double> product in LoadWarehouse("GROCERIES.TXT"))
            {
                Item newItem = new Item(0, product.Key, 0, Item.Category.OTHER, Item.Packaging.Unit, product.Value, 0);
                itemsByStock.Add(newItem);
            }

            return itemsByStock;
        }


        #region EnableCshiersOrCustomers
        public Person GetAvailableCustomer(Dictionary<string, Customer> customers)
        {
            foreach (KeyValuePair<string, Customer> pair in customers)
            {
                if (!pair.Value.Active)
                {
                    pair.Value.Active = true;
                    return pair.Value;
                }
            }

            return null;
        }
        #endregion

    }

}

