using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    public class Supermarket
    {

        #region ATRIBUTS
        private string name;
        private string address;
        public static int MAXLINES = 5;
        private int activeLines;
        private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
        private Dictionary<Item, double> ShoppingCart;
        public Dictionary<string, Person> Staff;
        public Dictionary<string, Person> Customers;
        public SortedDictionary<int, Item> Warehouse;

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

        public Supermarket(string name, string address, string fileCustomers, string fileItems, string fileGroceries, int activeLines) : this(name, address)
        {
            Customers = LoadCustomers("CUSTOMERS.TXT");
            Staff = LoadCashiers("CASHIERS.TXT");
            Warehouse = LoadWarehouse("GROCERIES.TXT");
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

        public Dictionary<string, Person> LoadCustomers(string fileName)
        {
            Dictionary<string, Person> aux = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);

            string line;
            line = sr.ReadLine();

            while (line != null)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 2)
                {
                    if (parts[2] is not "")
                    {
                        cust=new Customer(parts[0], parts[1], Convert.ToInt32(parts[2]));
                    }

                    else
                    {
                        cust = new Customer(parts[0], parts[1], null);
                    }
                    aux.Add(parts[0],cust);

                }
                line = sr.ReadLine();
            }
            sr.Close();
            return aux;
       }



        public Dictionary<string, Person> LoadCashiers(string fileName)
        {
            Dictionary<string, Person> aux = new Dictionary<string, Person>();
            StreamReader sr = new StreamReader(fileName);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 2)
                {
                    aux.Add(parts[0], new Cashier(parts[0], parts[1], Convert.ToDateTime(parts[3])));
                }
            }
            sr.Close();
            return aux;
        }
        


        public SortedDictionary<int, Item> LoadWarehouse(string fileName)
        {
            SortedDictionary<int, Item> aux = new SortedDictionary<int, Item>();
            StreamReader sr = new StreamReader(fileName);
            string line = sr.ReadLine(); 

            while (line != null)
            {
                string[] parts = line.Split(';');
                Category category = (Category)Convert.ToInt32(parts[1]);
                if (parts[2] == "K") pack = Packaging.Kg;
                else if (parts[2] == "U") pack = Packaging.Unit;
                else pack = Packaging.Package;
                    
                if (parts.Length >= 5)
                {
                        
                    aux.Add(Convert.ToInt32(parts[1]), new Item(Convert.ToInt32(parts[1]), parts[0],false, Convert.ToDouble(parts[3]),category,pack ,10,1));
                }
                    
            }           
            return aux;
        }
      
        #endregion

        private Packaging TranslateToPackaging(char packagingChar)
        {
            switch (packagingChar)
            {
                case 'K':
                    return Packaging.Kg;
                case 'U':
                    return Packaging.Unit;
                case 'P':
                    return Packaging.Package;
                default:
                    throw new ArgumentException("Invalid packaging character.");
            }

        }

        public SortedSet<Item> GetItemsByStock()
        {
            // Comparador personalizado para ordenar por stock
            IComparer<Item> stockComparer = Comparer<Item>.Create(new Comparison<Item>(CompareItemsByStock));

            // Conjunto ordenado de elementos por stock
            SortedSet<Item> itemsByStock = new SortedSet<Item>(stockComparer);

            // Agregar todos los elementos del almacén al conjunto
            foreach (KeyValuePair<int, Item> product in Warehouse)
            {
                itemsByStock.Add(product.Value);
            }
            return itemsByStock;
        }

            return itemsByStock;
        }*/


        #region EnableCshiersOrCustomers
        public Person GetAvailableCustomer()
        {
            Random r = new Random();
            Person selectedCustomer = null; 
            int llargada = Customers.Count();

            if (llargada == 0)
            {
                throw new Exception("NO HAY NINGÚN CLIENTE DISPONIBLE!!");
            }
            else
            {
                List<Person> availableCustomers = new List<Person>();

                // Agrega clientes disponibles a la lista
                foreach (KeyValuePair<string, Person> kvp in Customers)
                {
                    if (!kvp.Value.Active)
                    {
                        availableCustomers.Add(kvp.Value);
                    }
                }

                // Si no se encuentra ningún cliente disponible, devuelve null
                return null;


            return selectedCustomer;

        #endregion

            }
        }
    }
}

