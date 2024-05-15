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
        private Dictionary<string, Person> staff;
        private Dictionary<string, Person> customers;
        private SortedDictionary<int, Item> warehouse;

        #endregion

        //PROPIEDADES

        public SortedDictionary<int, Item> Warehouse { get { return warehouse; } }

        
        public Dictionary<string, Person> Staff { get { return staff; } }

        
        public Dictionary<string, Person> Customers { get { return customers; } }


        public CheckOutLine[] Lines
        {
            get { return lines; }
        }

        #region CONSTRUCTORS



        public Supermarket(string name, string address)
        {
            this.name = name;
            this.address = address;
            ActiveLines1 = 1;
            lines = new CheckOutLine[MAXLINES];
            staff = new Dictionary<string, Person>();
            InitializeCheckOutLines();
            ShoppingCart = new Dictionary<Item, double>();
            
        }

        public Supermarket(string name, string address, string fileCustomers, string fileItems, string fileGroceries, int activeLines) : this(name, address)
        {
            if (activeLines <= 0 || activeLines > MAXLINES) throw new Exception("Nombre de linies incorrecte");
            customers = LoadCustomers("CUSTOMERS.TXT");
            staff = LoadCashiers("CASHIERS.TXT");
            warehouse = LoadWarehouse("GROCERIES.TXT");
            this.ActiveLines1 = activeLines; // Actualizar el número de líneas activas
            for (int i = 0; i < activeLines; i++) lines[i] = new CheckOutLine(GetAvailableCashier(), i + 1);
            this.name = name;
            this.address = address;
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
            get { return ActiveLines1; }
        }

        public int ActiveLines1 { get => activeLines; set => activeLines = value; }
        public int ActiveLines2 { get => activeLines; set => activeLines = value; }
        #endregion


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
            SortedDictionary<int, Item> warehouse = new SortedDictionary<int, Item>();

            StreamReader sr = new StreamReader(fileName);

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(';');

                int id = warehouse.Count + 1;
                string description = parts[0];
                Category category = (Category)Convert.ToInt32(parts[1]);
                Packaging packaging = TranslateToPackaging(Convert.ToChar(parts[2]));
                double price = Convert.ToDouble(parts[3].Replace(',', '.'));

                Item newItem = new Item(id, description, true, price, category, packaging, 0, 1);

                warehouse.Add(id, newItem);
            }

            sr.Close();

            return warehouse;
        }


        #endregion

        #region Pkaging_Ordre_Items

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
            SortedSet<Item> itemsByStock = new SortedSet<Item>();

            if (Warehouse == null)
            {
                throw new InvalidOperationException("DICCIONARI BUIT");
            }

            foreach (KeyValuePair<int, Item> product in Warehouse)
            {
                itemsByStock.Add(product.Value);
            }

            return itemsByStock;
        }
        #endregion


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

            }
        }

        public Person GetAvailableCashier()
        {
            if (Staff == null)
            {
                throw new Exception("El diccionario de cajeros no ha sido inicializado.");
            }

            Random r = new Random();
            List<Person> availableCashier = new List<Person>();

            foreach (KeyValuePair<string, Person> kvp in Staff)
            {
                if (!kvp.Value.Active)
                {
                    availableCashier.Add(kvp.Value);
                }
            }

            if (availableCashier.Count == 0)
            {
                throw new Exception("NO HAY NINGÚN CASHIER DISPONIBLE!!");
            }

            return availableCashier[r.Next(availableCashier.Count)];       
        }
        #endregion

        #region CUES
        public void InitializeCheckOutLines()
        {
            for (int i = 0; i < MAXLINES; i++)
            {
                Person cashier = GetAvailableCashier();

                lines[i] = new CheckOutLine(cashier, i + 1);
            }
        }

        public void SetNumberOfActiveLines(int numberOfActiveLines)
        {
            if (numberOfActiveLines >= 0 && numberOfActiveLines <= MAXLINES)
            {
                ActiveLines1 = numberOfActiveLines;
            }
            else
            {
                Console.WriteLine("Número de líneas activas no válido.");
            }
        }
        public CheckOutLine GetCheckOutLine(int lineNumber)
        {
            CheckOutLine line = null;
            if (lineNumber >= 1 && lineNumber <= MAXLINES)
            {
                line = lines[lineNumber - 1];
            }
            return line;
        }

        public bool JoinTheQueue(ShoppingCart theCart, int line)
        {
            bool success = false; // Inicializamos la variable de éxito como false

            // Verificar si el número de línea está dentro de los límites del array
            if (line >= 1 && line <= MAXLINES)
            {
                // Obtener el índice correspondiente al número de línea
                int lineIndex = line - 1;

                // Verificar si la línea está activa
                if (lines[lineIndex] != null && lines[lineIndex].Active)
                {
                    // Agregar el carrito a la cola de la línea
                    lines[lineIndex].CheckIn(theCart);
                    success = true; // Marcamos la operación como exitosa
                }
                else
                {
                    // La línea de caja no está activa o es nula, mostrar un mensaje de error
                    Console.WriteLine($"La línea de caja {line} no está activa o es nula.");
                }
            }
            else
            {
                // El número de línea está fuera de los límites, mostrar un mensaje de error
                Console.WriteLine($"El número de línea {line} no es válido.");
            }

            return success; // Devolvemos el resultado de la operación
        }

        public bool Checkout(int line)
        {
            bool success = false; // Inicializamos la variable de éxito como false

            // Verificar si el número de línea está dentro de los límites del array
            if (line >= 1 && line <= MAXLINES)
            {
                // Obtener el índice correspondiente al número de línea
                int lineIndex = line - 1;

                // Realizar el checkout en la línea especificada
                success = lines[lineIndex].CheckOut();
            }

            return success; // Devolver el resultado de la operación de checkout
        }
        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{name}");
            sb.AppendLine($"{address}");

            // Iterar sobre cada línea de caja
            for (int i = 0; i < MAXLINES; i++)
            {
                // Obtener el número de la línea actual
                int lineNumber = i + 1;

                sb.AppendLine($"NUMERO DE CAIXA -> {lineNumber}");

                // Obtener la línea de caja actual
                CheckOutLine currentLine = lines[i];
                

                if (currentLine != null)
                {
                    // Obtener el cajero o cajera responsable de la línea
                    Person cashier = currentLine.Cashier;

                    sb.AppendLine($"CAIXER/A AL CARREC ->{cashier.FullName}");

                    // Obtener el contenido del carrito de compras de la línea
                    Queue<ShoppingCart> queue = currentLine.Queue;

                    if (queue.Count == 0)
                    {
                        sb.AppendLine("CUA BUIDA");
                    }
                    else
                    {
                        sb.AppendLine("************");

                        // Iterar sobre cada carrito de compras en la cola
                        foreach (ShoppingCart cart in queue)
                        {
                            sb.AppendLine(cart.ToString());
                            sb.AppendLine("********************");
                        }
                    }
                }
                else
                {
                    sb.AppendLine("CUA BUIDA");
                }
            }

            return sb.ToString();
        }
    }
}



