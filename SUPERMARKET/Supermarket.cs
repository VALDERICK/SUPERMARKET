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
            ActiveLines = activeLines; // Actualizar el número de líneas activas

            InitializeCheckOutLines(); // Inicializar las líneas de caja
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
            Customer cust;
            StreamReader sr = new StreamReader(fileName);

            string line;
            line = sr.ReadLine();

            while (line != null)
            {
                string[] parts = line.Split(';');

                if (parts[0] is not "CASH")
                {
                    if (parts[2] is not "")
                    {
                        cust = new Customer(parts[0], parts[1], Convert.ToInt32(parts[2]));
                    }

                    else
                    {
                        cust = new Customer(parts[0], parts[1], null);
                    }
                    aux.Add(parts[0], cust);

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
                string[] parts = line.Split(';');
                string[] extracHora = parts[3].Split(" ");
                string[] dataFinal = extracHora[0].Split("/");

                DateTime hire = new DateTime(Convert.ToInt32(dataFinal[2]), Convert.ToInt32(dataFinal[1]), Convert.ToInt32(dataFinal[0]));
                Cashier cashier = new Cashier(parts[0], parts[1], hire);

                aux.Add(parts[0], cashier);


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
                Packaging pack = TranslateToPackaging(Convert.ToChar(parts[2]));

                double price = Convert.ToDouble(parts[3].Replace(',', '.')); // Reemplaza la coma por un punto en el precio

                aux.Add(aux.Count + 1, new Item(aux.Count + 1, parts[0], true, price, category, pack, 10, 1));

                line = sr.ReadLine(); // Lee la siguiente línea del archivo
            }
            sr.Close();
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

        public SortedSet<Item> GetItemByStock()
        {
            SortedSet<Item> itemsByStock = new SortedSet<Item>(Comparer<Item>.Create((item1, item2) => item1.Stock.CompareTo(item2.Stock)));

            foreach (KeyValuePair<int, Item> product in LoadWarehouse("GROCERIES.TXT"))
            {

                Item newItem = new Item(0, product.Value.Description, false, 0, Category.OTHER, Packaging.Unit, 10, 0);

                itemsByStock.Add(newItem);
            }
            return itemsByStock;
        }



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

                if (availableCustomers.Count == 0)
                {
                    return null;
                }

                selectedCustomer = availableCustomers[r.Next(availableCustomers.Count)];

                return selectedCustomer;

                #endregion

            }
        }

        public void OpenCheckOutLine(int line2Open)
        {
            // Asegurarse de que line2Open está dentro del rango válido
            if (line2Open < 1 || line2Open > MAXLINES)
            {
                Console.WriteLine("Invalid line number. Must be between 1 and MAXLINES.");
                return;
            }

            // Decrementar 1 unidad para ajustar al índice del array
            int lineIndex = line2Open - 1;

            // Verificar si la línea ya está abierta
            if (lines[lineIndex] != null)
            {
                Console.WriteLine($"Line {line2Open} is already open.");
                return;
            }

            // Crear una nueva línea de caja y asignarla al array de líneas
            lines[lineIndex] = new CheckOutLine();

            // Asignar un cajero disponible a la nueva línea de caja
            lines[lineIndex].cashier = GetAvailableCustomer();

            // Asignar un número de línea secuencial
            lines[lineIndex].number = line2Open;

            Console.WriteLine($"CheckOutLine {line2Open} has been opened with cashier {lines[lineIndex].cashier.FullName}.");
        }


    }
}



