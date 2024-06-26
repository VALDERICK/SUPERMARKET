﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SUPERMARKET.Item;

    public enum Category
    { BERVERAGE = 1, FRUITS, VEGETABLES, BREAD, MILK_AND_DERIVATES, GARDEN, MEAT, SWEETS, SAUCES, FROZEN, CLEANING, FISH, OTHER };

    public enum Packaging
    { Unit, Kg, Package };
    namespace SUPERMARKET
    {

    public class Item : IComparable<Item>
    {

        #region ENUMS
       
        #endregion

        #region ATRIBUTS
        char currency = '\u20AC';
        int code;
        string description;
        bool onSale;
        double price;
        Category category;
        Packaging packaging;
        double stock;
        int minStock;
        #endregion

        #region CONSTRUCTORS
        public Item(int _code, string _description,bool _onSale,  double _price, Category _category, Packaging _packaging, double _stock, int _minStock)
        {
            code = _code;
            description = _description;
            price = _price;
            category = _category;
            packaging = _packaging;
            stock = _stock;
            minStock = _minStock;
            onSale = _onSale;
             


            if (_stock <= 0)
            {
                Random r = new Random();
                stock += r.Next(0,10000);
            }
            else
            {
                stock = _stock;
            }
            if (_minStock <= 0)
            {
                minStock += 1;
            }
            else
            {
                minStock = _minStock;
            }
        }
        #endregion

        #region GETTERS

        public double Code
        {
            get
            {
                return code;
            }
        }

        public double Stock
        {
            get
            {
                return stock;
            }
        }

        public int MinStock
        {
            get
            {
                return minStock;
            }
        }

        public Packaging PackagingType
        {
            get
            {
                return packaging;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public Category GetCategory
        {
            get
            {
                return category;
            }
        }

        public bool OnSale
        {
            get
            {
                if (stock > stock / 2)
                {
                    onSale = true;
                }
                else onSale = false;

                return onSale;
            }
        }
        #endregion

        #region METODES

        public static void UpdateStock(Item item, double qty)
        {
            //si es una cantidad de stock que retiran, la cantidad sera -5, tipo retiran 5 unidades
            item.stock += qty;
        }

        #endregion

        #region PROPIETATS
        public double Price
        {
            get
            {
                double precioFinal;

                if (onSale)
                {
                    precioFinal= price * 0.10;
                    price = price - precioFinal;
                    precioFinal = price;
                }
                else
                {
                    precioFinal= price; 
                }

                return precioFinal;
            }
        }

        
        #endregion

        #region ToString
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"CODE->{code}  DESCRIPTION->{description}  CATEGORY-> {category}");
            sb.Append($" STOCK->{stock}    MIN_STOCK->{minStock}   PRICE->{Price}€  ON SALE->");

            if (onSale)
            {
                double discountedPrice = price * 0.10;
                
                sb.Append($"Y (Descuento: {discountedPrice}€)");
            }
            else
            {
                sb.Append("N");
            }

            return sb.ToString();

        }
        #endregion
        public int CompareTo(Item? other)
        {
            int resultat;
            if (other == null)
            {
                resultat= 1; 
            }

            resultat= this.stock.CompareTo(other.Stock);
            if (resultat == 0)
                resultat = code.CompareTo(other.code);
            return resultat;
        }
        public override bool Equals(object obj)
        {
            bool resultado = false;

            if (obj != null && GetType() == obj.GetType())
            {
                Item otherItem = (Item)obj;

                resultado = this.code == otherItem.code &&
                            this.category == otherItem.category &&
                            this.description == otherItem.description;
            }

            return resultado;
        }
    }
}
