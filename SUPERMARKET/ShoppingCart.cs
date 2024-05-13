﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class ShoppingCart
    {
        // ATRIBUTOS

        private Dictionary<Item, double> shoppingList;
        private Customer customer;
        private DateTime dateOfPurchase = DateTime.Now;

        //CONSTRUCTOR

        public ShoppingCart(Customer customer, DateTime dateOfPurchase)
        {
            this.customer = customer;
            this.dateOfPurchase = dateOfPurchase;
            customer.Active = true;
            shoppingList = new Dictionary<Item, double>();
        }

        //PROPIEDADES

        public Dictionary<Item, double> ShoppingList
        {
            get { return shoppingList; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public DateTime DateOfPurchase
        {
            get { return dateOfPurchase; }
        }

        //METODOS

        public void AddOne(Item item, double qty)
        {
            
            if (shoppingList.ContainsKey(item))
            {
                
                if (item.PackagingType == Packaging.Unit || item.PackagingType == Packaging.Package)
                {
                    qty = Math.Round(qty); 
                }
                shoppingList[item] += qty; 
            }
            else
            {
                
                if (item.PackagingType == Packaging.Unit || item.PackagingType == Packaging.Package)
                {
                    qty = Math.Round(qty); 
                }
                shoppingList.Add(item, qty); 
            }

        }
        public void AddAllRandomly(SortedDictionary<int, Item> warehouse)
        {
            Random random = new Random();
            int numItems = random.Next(1, 11);
            int contador = 0;

            foreach (var kvp in warehouse) 
            {
                if (contador >= numItems) 
                    break;
                double qty = random.Next(1, 6); 
                AddOne(kvp.Value, qty); 
                contador++;
            }
        }
        public static double ProcessItems(ShoppingCart cart)
        {
            double totalInvoiced = 0;         
            foreach (KeyValuePair<Item, double> kvp in cart.ShoppingList)
            {
                totalInvoiced += kvp.Key.Price * kvp.Value; 
            }           
            double points = Math.Truncate(totalInvoiced * 0.01);

            return points;
        }

    }
}
