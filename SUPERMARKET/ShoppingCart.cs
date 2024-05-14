using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    public class ShoppingCart
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

            if (warehouse != null && warehouse.Count > 0)
            {
                foreach (var kvp in warehouse)
                {
                    if (contador >= numItems)
                        break;
                    double qty = random.Next(1, 6);
                    AddOne(kvp.Value, qty);
                    contador++;
                }
            }
            else
            {
                Console.WriteLine("El almacén está vacío o no contiene elementos.");
            }
        }
        public int RawPointsObtainedAtCheckout(double totalInvoiced)
        {
            
            int PuntObtinguts = (int)(totalInvoiced / 100);

            return PuntObtinguts;
        }

        public static double ProcessItems(ShoppingCart carro)
        {
            double granTotalFacturado = 0;

           
            foreach (KeyValuePair<Item, double> par in carro.shoppingList)
            {
                Item producto = par.Key;
                double cantidadEnCarrito = par.Value;

                // Decidir la cantidad final del producto comprado
                double cantidadFinal = cantidadEnCarrito;

                // Obtener el stock actual del producto
                double stockDisponible = producto.Stock;

                // Si el stock del artículo es menor que la cantidad en el carrito,
                // ajustar la cantidad al stock actual
                if (stockDisponible < cantidadEnCarrito)
                {
                    cantidadFinal = stockDisponible;
                }

                // Disminuir el stock del producto
                Item.UpdateStock(producto, stockDisponible - cantidadFinal);

                // Calcular el precio total del producto y acumularlo al gran total facturado
                double precioTotalProducto = cantidadFinal * producto.Price;
                granTotalFacturado += precioTotalProducto;
            }

            // Retornar el gran total facturado con 2 decimales
            return Math.Round(granTotalFacturado, 2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***********");
            sb.AppendLine($"INFO CARRITO DE COMPRA CLIENT-> {customer.FullName}");

            foreach (KeyValuePair<Item, double> par in shoppingList)
            {
                Item producto = par.Key;
                double cantidad = par.Value;

                sb.AppendLine($"{producto.Description} -CAT→{producto.GetCategory} -QTY→{cantidad} -UNIT PRICE → {producto.Price} €");
            }

            sb.AppendLine("****FI CARRITO COMPRA****");

            return sb.ToString();
        }





    }
}
