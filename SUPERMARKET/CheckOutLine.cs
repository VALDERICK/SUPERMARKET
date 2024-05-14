using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    public class CheckOutLine
    {
        //ATRIBUTS
        public int number;
        public Queue<ShoppingCart> queue;
        public Person cashier;
        public bool active;

        //CONSTRUCTOR
        public CheckOutLine(Person cashier, int number)
        {
            this.cashier = cashier;
            this.number = number;
            this.queue = new Queue<ShoppingCart>();
            this.active = true;
        }
        //METODOS
        public bool CheckIn(ShoppingCart oneShoppingCart)
        {
            bool hoEsta = false;
            if (!active)
            {
                hoEsta = false; // La línea de caja no está activa, no se puede hacer check-in
            }
            else
            {
                queue.Enqueue(oneShoppingCart); // Agregar el carrito de compras a la cola
                hoEsta = true;
            }           
            return hoEsta ;
        }

        public bool CheckOut()
        {
            if (!active || queue.Count == 0)
            {
                return false; // La línea de caja no está activa o no hay ningún carrito en la cola
            }

            ShoppingCart currentCart = queue.Dequeue(); // Sacar el carrito de compras de la cola

            // Procesar los artículos del carrito de compras y obtener el monto bruto de la compra
            double grossAmount = ShoppingCart.ProcessItems(currentCart);

            // Obtener los puntos brutos asociados con el carrito de compras
            int rawPoints = currentCart.RawPointsObtainedAtCheckout(grossAmount);

            // Agregar el monto de la factura procesada al cajero y al cliente
            cashier.AddInvoiceAmount(grossAmount);
            currentCart.Customer.AddInvoiceAmount(grossAmount);

            // Agregar puntos al cliente y al cajero de la línea
            currentCart.Customer.AddPoints(rawPoints);
            cashier.AddPoints(rawPoints);

            // Establecer la propiedad Active del cliente como false
            currentCart.Customer.Active = false;

            return true;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"NUMERO DE CAIXA -> {number}");
            sb.AppendLine($"CAIXER/A AL CARREC -> {cashier.FullName}");

            if (queue.Count == 0)
            {
                sb.AppendLine("CUA BUIDA");
            }
            else
            {               
                foreach (ShoppingCart shoppingCart in queue)
                {
                    sb.AppendLine(shoppingCart.ToString());
                }                
            }

            return sb.ToString();
        }
    }
}
