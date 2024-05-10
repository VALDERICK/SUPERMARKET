using System;
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
                
                if (item.PackagingType == Item.Packaging.Unit || item.PackagingType == Item.Packaging.Package)
                {
                    qty = Math.Round(qty); 
                }
                shoppingList[item] += qty; 
            }
            else
            {
                
                if (item.PackagingType == Item.Packaging.Unit || item.PackagingType == Item.Packaging.Package)
                {
                    qty = Math.Round(qty); 
                }
                shoppingList.Add(item, qty); 
            }
        }
    }
}
