﻿using System;
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
        private Dictionary<string, Person> staff;
        private Dictionary<string, Person> customers;
        private SortedDictionary<int, Item> warehouse;

        public class CheckOutLine
        {
            private int number;
            //private Queue<ShoppingCart> queue;
            private Person cashier;
            private bool active;
        }

        public Supermarket(string name, string address, string fileCustomers, string fileItems, int activeLines) : this(name, address)
        {
            LoadCustomers("CUSTOMERS.TXT");
            LoadCashiers("CASHIERS.TXT");
            LoadWarehouse("GROCERIES.TXT");
            this.activeLines = activeLines;
        }

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

        public void InitializeCheckOutLines()
        {
            for (int i = 0; i < MAXLINES; i++)
            {
                lines[i] = new CheckOutLine(); // Initialize each checkout line
            }
        }

        private Dictionary<string, string> LoadCustomers(string fileName)
        {
            Dictionary<string, string> customers = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(fileName);

            string line;
            line = sr.ReadLine();
            while (sr != null)
            {
                string[] parts = line.Split(',');

                customers.Add(parts[0], parts[1]);
            }

            return customers;
        }

        private Dictionary<string, string> LoadCashiers(string fileName)
        {
            Dictionary<string, string> cashiers = new Dictionary<string, string>();
            StreamReader sr = new StreamReader(fileName);

            string line;
            line = sr.ReadLine();
            while (sr != null)
            {
                string[] parts = line.Split(',');

                cashiers.Add(parts[0], parts[1]);
            }

            return cashiers;
        }

        private Dictionary<string, double> LoadWarehouse(string fileName)
        {
            Dictionary<string, double> products = new Dictionary<string, double>();
            StreamReader sr = new StreamReader(fileName);

            string line;
            line = sr.ReadLine();
            while (sr != null)
            {
                string[] parts = line.Split(',');
                products.Add(parts[0], Convert.ToDouble(parts[4]));
            }

            return products;
        }
    }
}