//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SUPERMARKET
//{
//    internal class Supermarket
//    {
//        #region ATRIBUTS
//        private string name;
//        private string address;
//        public static int MAXLINES = 5;
//        private int activeLines;
//        private CheckOutLine[] lines = new CheckOutLine[MAXLINES];
//        private Dictionary<string, Person> staff;
//        private Dictionary<string, Person> customers;
//        private SortedDictionary<int, Item> warehouse;

//        #endregion

//        #region METHODS
//        private Dictionary<string, Person> LoadCustomers(string fileName)
//        {
//            StreamReader sr = new StreamReader(fileName);
//            string linia;

//            linia = sr.ReadLine();

//            while (linia != null)
//            {
//                string[] infoClient = linia.Split(';');
//                string id = infoClient[0];
//                string nomComplet = infoClient[1].Trim();
//                string tarjetaFidelitat;
//                if (infoClient.Length > 2)
//                {
//                    tarjetaFidelitat = infoClient[2];
//                }
//                else
//                {
//                    tarjetaFidelitat = "";
//                }

//                Person customer = new Person(id, nomComplet, tarjetaFidelitat);
//                customers[id] = customer;
//            }

//        }
//        private Dictionary<string, Person> LoadCashiers(string fileName)
//        {

//        }
//        private Dictionary<string, Person> LoadWarehouse(string fileName)
//        {

//        }


//        #endregion
//        #region CONSTRUCTORS
//        public Supermarket(string name, string address, string fileCashiers, string fileCostumers, string fileItems, int activeLines)
//        {
//            this.name = name;
//            this.address = address;
//            activeLines = 1; // Default to 1 active line
//            lines = new CheckOutLine[MAXLINES]; // Initialize checkout lines
//        }
//        #endregion



//        Properties
//        public string Name
//        {
//            get { return name; }
//            set { name = value; }
//        }

//        public string Address
//        {
//            get { return address; }
//            set { address = value; }
//        }

//        public int ActiveLines
//        {
//            get { return activeLines; }
//            set
//            {
//                if (value >= 1 && value <= MAXLINES)
//                {
//                    activeLines = value;
//                }
//                else
//                {
//                    throw new ArgumentException("Active lines must be between 1 and MAXLINES.");
//                }
//            }
//        }

//        Method to initialize checkout lines
//        public void InitializeCheckOutLines()
//        {
//            for (int i = 0; i < MAXLINES; i++)
//            {
//                lines[i] = new CheckOutLine(); // Initialize each checkout line
//            }
//        }
//    }

//}
