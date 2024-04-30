using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Item : IComparable<Item>
    {

        #region ENUMS
        public enum Category
        { BERVERAGE = 1, FRUITS, VEGETABLES, BREAD, MILK_AND_DERIVATES, GARDEN, MEAT, SWEETS, SAUCES, FROZEN, CLEANING, FISH, OTHER };

        public enum Packaging
        {Unit, Kg, Package};
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

        public int CompareTo(Item? other)
        {
            throw new NotImplementedException();
        }
    }
}
