using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace SUPERMARKET
{
    internal class Supermarket
    {
        // Attributes
        private string name;
        private string address;
        public static int MAXLINES = 5; // Maximum number of queues
        private int activeLines; // Number of active queues
        private CheckOutLine[] lines =new CheckOutLine[MAXLINES]; // Array to store checkout lines
        private Dictionary<Item, double> ShoppingCart; // Shopping cart of some customer

        // Constructor

        public class CheckOutLine
        {
            private int number;
            private Queue<ShoppingCart> queue;
            private Person cashier;
            private bool active;
        }

        public Supermarket(string name, string address)
        {
            this.name = name;
            this.address = address;
            activeLines = 1; // Default to 1 active line
            lines = new CheckOutLine[MAXLINES]; // Initialize checkout lines
            ShoppingCart = new Dictionary<Item, double>(); // Initialize shopping cart
        }

        // Properties
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

        // Method to initialize checkout lines
        public void InitializeCheckOutLines()
        {
            for (int i = 0; i < MAXLINES; i++)
            {
                lines[i] = new CheckOutLine(); // Initialize each checkout line
            }
        }
    }

}*/

    