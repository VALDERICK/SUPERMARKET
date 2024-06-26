﻿using System;
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

        //PROPIETATS

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public Queue<ShoppingCart> Queue
        {
            get { return queue; }
            set { queue = value; }
        }

        public Person Cashier
        {
            get { return cashier; }
            set { cashier = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public bool Empty
        {
            get
            {
                bool isempty=false;
                if (Queue.Count == 0)
                {
                    isempty=true;
                }
                else
                {
                    isempty=false;
                }
                return isempty;
            }
        }

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
            if (active) queue.Enqueue(oneShoppingCart);
            return active;
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

            if (active == false)
            {
                sb.AppendLine("CUA TANCADA");

            }
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
