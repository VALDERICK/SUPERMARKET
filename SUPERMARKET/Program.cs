using System;
using System.Collections.Generic;

namespace SUPERMARKET
{
    internal class Program
    {
        public static void MostrarMenu()
        {
            Console.WriteLine("1- UN CLIENT ENTRA AL SUPER I OMPLE EL SEU CARRO DE LA COMPRA");
            Console.WriteLine("2- AFEGIR UN ARTICLE A UN CARRO DE LA COMPRA");
            Console.WriteLine("3- UN CARRO PASSA A CUA DE CAIXA (CHECKIN)");
            Console.WriteLine("4- CHECKOUT DE CUA TRIADA PER L'USUARI");
            Console.WriteLine("5- OBRIR SEGÜENT CUA DISPONIBLE");
            Console.WriteLine("6- INFO CUES");
            Console.WriteLine("7- CLIENTS VOLTANT PEL SUPERMERCAT");
            Console.WriteLine("8- LLISTAR CLIENTS PER RATING (DESCENDENT)");
            Console.WriteLine("9- LLISTAR ARTICLES PER STOCK (DE  - A  +)");
            Console.WriteLine("A- CLOSE QUEUE");
            Console.WriteLine("0- EXIT");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            
            Supermarket super = new Supermarket("HIPERCAR", "C/Barna 99", "CASHIERS.TXT", "CUSTOMERS.TXT", "GROCERIES.TXT", 2);
            
            Dictionary<Customer, ShoppingCart> carrosPassejant = new Dictionary<Customer, ShoppingCart>();

            ConsoleKeyInfo tecla;
            do
            {
                Console.Clear();
                MostrarMenu();
                tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        DoNewShoppingCart(carrosPassejant, super);
                        break;
                    case ConsoleKey.D2:
                        DoAfegirUnArticleAlCarro(carrosPassejant, super);

                        break;
                    case ConsoleKey.D3:
                        DoCheckIn(carrosPassejant, super);

                        break;
                    case ConsoleKey.D4:
                        if (DoCheckOut(super)) Console.WriteLine("BYE BYE. HOPE 2 SEE YOU AGAIN!");
                        else Console.WriteLine("NO S'HA POGUT TANCAR CAP COMPRA");

                        break;
                    case ConsoleKey.D5:
                        DoOpenCua(super);

                        break;
                    case ConsoleKey.D6:
                        DoInfoCues(super);

                        break;

                    case ConsoleKey.D7:
                        DoClientsComprant(carrosPassejant);


                        break;
                    case ConsoleKey.D8:
                        DoListCustomers(super);

                        break;

                    case ConsoleKey.D9:
                        SortedSet<Item> articlesOrdenatsPerEstoc = super.GetItemByStock();
                        DoListArticlesByStock("LLISTAT D'ARTICLES - DATA " + DateTime.Now, articlesOrdenatsPerEstoc);

                        break;
                    case ConsoleKey.A:
                        DoCloseQueue(super);

                        break;

                    case ConsoleKey.D0:
                        MsgNextScreen("PRESS ANY KEY 2 EXIT");
                        break;
                    default:
                        MsgNextScreen("Error. Prem una tecla per tornar al menú...");
                        break;
                }

            } while (tecla.Key != ConsoleKey.D0);


        }
        //OPCIO 1 - Entra un nou client i se li assigna un carro de la compra. S'omple el carro de la compra
        /// <summary>
        /// Crea un nou carro de la compra assignat a un Customer inactiu
        /// L'omple d'articles aleatòriament 
        /// i l'afegeix als carros que estan passejant pel super
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder seleccionar un client inactiu</param>
        public static void DoNewShoppingCart(Dictionary<Customer, ShoppingCart> carros, Supermarket super)
        {
            Console.Clear();

            Person inactivePerson = super.GetAvailableCustomer();

            if (inactivePerson != null && inactivePerson is Customer)
            {

                Customer inactiveCustomer = (Customer)inactivePerson;

                ShoppingCart newCart = new ShoppingCart(inactiveCustomer, DateTime.Now);

                super.LoadWarehouse("GROCERIES.TXT");

                newCart.AddAllRandomly(super.Warehouse);

                carros.Add(inactiveCustomer, newCart);

                Console.WriteLine(newCart.ToString());

                Console.WriteLine("Se ha creado un nuevo carro de la compra y se ha asignado al cliente inactivo.");
            }
            else
            {
                Console.WriteLine("No hay clientes inactivos disponibles para asignar un nuevo carro de la compra o el cliente no es del tipo Customer.");
            }


            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
        }

        //OPCIO 2 - AFEGIR UN ARTICLE ALEATORI A UN CARRO DE LA COMPRA ALEATORI DELS QUE ESTAN VOLTANT PEL SUPER
        /// <summary>
        /// Dels carros que van passejant pel super, 
        /// es selecciona un carro a l'atzar i un article a l'atzar
        /// i s'afegeix al carro de la compra
        /// amb una quantitat d'unitats determinada
        /// Cal mostrar el carro seleccionat abans i després d'afegir l'article.
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder seleccionar un article del magatzem</param>
        public static void DoAfegirUnArticleAlCarro(Dictionary<Customer, ShoppingCart> carros, Supermarket super)
        {
            Console.Clear();
            Random random = new Random();
            Person activePerson = super.GetAvailableCustomer();

            if (activePerson is Customer)
            {
                Customer activeCustomer = (Customer)activePerson;

                if (carros.Count > 0)
                {
                    KeyValuePair<Customer, ShoppingCart> randomCartEntry = carros.ElementAt(random.Next(carros.Count));
                    ShoppingCart randomCart = randomCartEntry.Value;

                    Console.WriteLine($"CARRO ORIGINAL:{randomCart}");

                    if (super.Warehouse.Count > 0)
                    {
                        int randomItemId = random.Next(0, super.Warehouse.Count);
                        Item randomWarehouseItem = super.Warehouse[randomItemId];

                        double quantity = random.Next(1, 6);
                        randomCart.AddOne(randomWarehouseItem, quantity);

                        Console.WriteLine($"CARRO MODIFICADO:{randomCart}");
                    }
                    else
                    {
                        Console.WriteLine("El almacén está vacío. No se pueden agregar artículos al carro.");
                    }
                }
                else
                {
                    Console.WriteLine("No hay carros de compra disponibles para agregar un artículo.");
                }
            }
            else
            {
                Console.WriteLine("No hay clientes inactivos disponibles para asignar un nuevo carro de la compra o el cliente no es del tipo Customer.");
            }

            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
        }

        // OPCIO 3 : Un dels carros que van pululant pel super  s'encua a una cua activa
        // La selecció del carro i de la cua és aleatòria
        /// <summary>
        /// Agafem un dels carros passejant (random) i l'encuem a una de les cues actives
        /// de pagament.
        /// També hem d'eliminar el carro seleccionat de la llista de carros que passejen 
        /// Si no hi ha cap carro passejant o no hi ha cap linia activa, cal donar missatge 
        /// 
        /// </summary>
        /// <param name="carros">Llista de carros que encara no han entrat a cap 
        /// cua de pagament</param>
        /// <param name="super">necessari per poder encuar un carro a una linia de caixa</param>
        public static void DoCheckIn(Dictionary<Customer, ShoppingCart> carros, Supermarket super)
        {
            Console.Clear();
            try
            {
                Random random = new Random();
                Customer selectedCustomer;
                CheckOutLine selectedLine;
                ShoppingCart selectedShoppingCart;
                int selectedLineIndex = random.Next(0, super.ActiveLines);
                int randomIndex = random.Next(0, carros.Count);
                bool success = false;

                // Verificar si no hay líneas de caja abiertas
                if (super.ActiveLines <= 0)
                {
                    throw new Exception("NO HAY NINGUNA LÍNEA DE CAJA ABIERTA");
                }
                // Verificar si no hay carros disponibles para encolar
                if (carros.Count == 0)
                {
                    throw new Exception("NO HAY NINGÚN CARRO DISPONIBLE PARA ENCOLAR");
                }

                selectedCustomer = carros.Keys.ElementAt(randomIndex);
                selectedShoppingCart = carros[selectedCustomer];

                // Verificar si el cliente seleccionado está activo (no ha sido atendido todavía)
                if (!selectedCustomer.Active)
                {
                    throw new Exception("EL CLIENTE SELECCIONADO YA HA SIDO ATENDIDO");
                }

                // Seleccionar aleatoriamente una línea de caja activa
                selectedLine = super.GetCheckOutLine(selectedLineIndex + 1);  

        
                // Encolar el carro en la línea de caja seleccionada
                success = selectedLine.CheckIn(selectedShoppingCart);

                if (success)
                {
                    carros.Remove(selectedCustomer);
                    Console.WriteLine(super.ToString());
                }
                else
                {
                    Console.WriteLine($"ERROR AL ENCOLAR EL CARRO DE {selectedCustomer} EN LA LÍNEA DE CAJA {selectedLine.Number}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            MsgNextScreen("PRESIONA UNA TECLA PARA VOLVER AL MENÚ PRINCIPAL");

        }



        // OPCIO 4 - CHECK OUT D'UNA CUA TRIADA PER L'USUARI
        /// <summary>
        /// Es demana per teclat una cua de les actives (1..ActiveLines)
        /// i es fa el checkout del ShoppingCart que toqui
        /// Si no hi ha cap carro a la cua triada, es dona un missatge
        /// </summary>
        /// <param name="super">necessari per fer el checkout</param>
        /// <returns>true si s'ha pogut fer el checkout. False en cas contrari</returns>

        public static bool DoCheckOut(Supermarket super)
        {
            Console.Clear();
            bool fet = false;
            string input;
            // Verificar si hay colas activas
            if (super.ActiveLines <= 0)
            {
                Console.WriteLine("NO HAY NINGUNA LÍNEA DE CAJA ABIERTA");
            }

            // Mostrar las colas activas y la información de la caja
            Console.WriteLine("Colas activas:");
            for (int i = 0; i < super.ActiveLines; i++)
            {
                CheckOutLine currentLine = super.GetCheckOutLine(i + 1); // +1 porque los índices comienzan desde 1
                if (currentLine.Active)
                {
                    Console.WriteLine($"Caja {currentLine.Number}: {currentLine}");
                }
            }

            // Pedir al usuario que seleccione una cola para hacer el checkout
            Console.Write("Selecciona el número de la cola para hacer el checkout: ");
            input = Console.ReadLine();

            if (!int.TryParse(input, out int selectedLine) || selectedLine < 1 || selectedLine > super.ActiveLines)
            {
                Console.WriteLine("Número de cola no válido.");
            }
            else
            {
                fet = super.Checkout(selectedLine);

                if (fet)
                {
                    Console.WriteLine("Checkout realizado con éxito.");
                }
                else
                {
                    Console.WriteLine("No se pudo realizar el checkout en la cola seleccionada.");
                }
            }

             MsgNextScreen("PRESIONA UNA TECLA PARA VOLVER AL MENÚ PRINCIPAL");
            return fet;
        }
        /// <summary>
        /// En cas que hi hagin cues disponibles per obrir, s'obre la 
        /// següent linia disponible
        /// </summary>
        /// <param name="super"></param>
        /// <returns>true si s'ha pogut obrir la cua</returns>
        // OPCIO 5 : Obrir la següent cua disponible (si n'hi ha)
        public static bool DoOpenCua(Supermarket super)
        {
            Console.Clear();

            bool success = false; // Variable para indicar si se pudo abrir la siguiente cola disponible

            // Verificar si hay colas disponibles para abrir
            if (super.ActiveLines < Supermarket.MAXLINES)
            {
                // Incrementar el número de líneas activas
                super.ActiveLines++;

                // Abrir la siguiente cola disponible
                int newLineIndex = super.ActiveLines - 1; // Índice de la nueva línea de caja
                super.Lines[newLineIndex] = new CheckOutLine(super.GetAvailableCashier(), super.ActiveLines);

                Console.WriteLine("Se ha abierto la siguiente cola disponible.");
                success = true;
            }
            else
            {
                Console.WriteLine("No hay más colas disponibles para abrir.");
            }

            MsgNextScreen("Presiona una tecla para volver al menú principal");

            return success;
        }


        //OPCIO 6 : Llistar les cues actives
        /// <summary>
        /// Es llisten totes les cues actives des de la 1 fins a ActiveLines.
        /// Apretar una tecla després de cada cua activa
        /// </summary>
        /// <param name="super"></param>
        public static void DoInfoCues(Supermarket super)
        {
            Console.Clear();

            // Iterar sobre cada línea de caja activa
            for (int i = 0; i < super.ActiveLines; i++)
            {
                // Obtener el número de la línea actual
                int lineNumber = i + 1;

                // Obtener la línea de caja actual
                CheckOutLine currentLine = super.GetCheckOutLine(lineNumber);

                // Mostrar información sobre la línea de caja actual
                Console.WriteLine($"Información de la línea de caja {lineNumber}:");
                Console.WriteLine(currentLine.ToString());

            }

            MsgNextScreen("Presiona una tecla para volver al menú principal");

        }


        // OPCIO 7 - Mostrem tots els carros de la compra que estan voltant
        // pel super però encara no han anat a cap cua per pagar
        /// <summary>
        /// Es mostren tots els carros que no estan en cap cua.
        /// Cal apretar una tecla després de cada carro
        /// </summary>
        /// <param name="carros"></param>
        public static void DoClientsComprant(Dictionary<Customer, ShoppingCart> carros)
        {
            Console.Clear();
            Console.WriteLine("Carros de la compra en movimiento por el supermercado:");

            if (carros.Count == 0)
            {
                Console.WriteLine("No hay carros de la compra en movimiento en este momento.");
            }
            else
            {
                foreach (KeyValuePair<Customer, ShoppingCart> kvp in carros)
                {
                    Console.WriteLine($"Cliente: {kvp.Key.FullName}, Carro: {kvp.Value}");
                }
            }

            MsgNextScreen("Presiona cualquier tecla para volver al menú principal");
        }

        //OPCIO 8 : LListat de clients per rating
        /// <summary>
        /// Cal llistar tots els clients de més a menys rating
        /// Per poder veure bé el llistat, primer heu de fer uns quants
        /// checkouts i un cop fets, fer el llistat. Aleshores els
        /// clients que han comprat tindran ratings diferents de 0
        /// Jo he fet una sentencia linq per solucionar aquest apartat
        /// </summary>
        /// <param name="super"></param>
        public static void DoListCustomers(Supermarket super)
        {

            Console.Clear();

            // Filtrar clientes con rating mayor que 0 y ordenarlos por rating de forma descendente
            List<Customer> filteredCustomers = new List<Customer>();
            foreach (Customer customer in super.Customers.Values)
            {
                if (customer.GetRating > 0)
                {
                    filteredCustomers.Add(customer);
                }
            }

            // Mostrar la lista de clientes ordenados por rating
            Console.WriteLine("Lista de clientes ordenados por rating:");
            foreach (Customer customer in filteredCustomers)
            {
                Console.WriteLine(customer.ToString());
            }

            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

        }

        // OPCIO 9
        /// <summary>
        /// Llistar de menys a més estoc, tots els articles del magatzem
        /// </summary>
        /// <param name="header">Text de capçalera del llistat</param>
        /// <param name="items">articles que ja vindran preparats en la ordenació desitjada</param>
        public static void DoListArticlesByStock(String header, SortedSet<Item> items)
        {
            Console.Clear();
            Console.WriteLine(header);

            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }

            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
        }

        // OPCIO A : Tancar cua. Només si no hi ha cap client
        /// <summary>
        /// Començant per la última cua disponible, tanca la primera que trobi sense
        /// cap carro encuat. (primer mirem la número "ActiveLines" després ActiveLines-1 ...
        /// Fins trobar una que estigui buida. La que trobem la eliminarem
        /// Cal afegir la propietat Empty a la classe ChecOutLine i  a la classe SuperMarket:
        /// el mètode public static bool RemoveQueue(Supermarket super, int lineToRemove)
        /// que elimina la cua amb número = lineToRemove i retorna true en cas que l'hagi 
        /// pogut eliminar (perquè no hi ha cap carro pendent de pagament)
        /// </summary>
        /// <param name="super"></param>
        public static void DoCloseQueue(Supermarket super)
        {
            Console.Clear();

            bool queueClosed = false;

            // Iterar desde la última cola disponible hasta la primera
            for (int i = super.ActiveLines - 1; i >= 0; i--)
            {
                CheckOutLine line = super.GetCheckOutLine(i + 1); // Obtener la línea de caja actual

                // Verificar si la cola está vacía (sin carros pendientes)
                if (line.Empty)
                {
                    // Eliminar la cola
                    bool removed = Supermarket.RemoveQueue(super, i + 1);
                    if (removed)
                    {
                        Console.WriteLine($"Se ha cerrado la cola {i + 1}.");
                        queueClosed = true;
                        break; // Salir del bucle una vez que se cierre la primera cola vacía
                    }
                }
            }

            if (!queueClosed)
            {
                Console.WriteLine("No se encontró ninguna cola vacía para cerrar.");
            }

            MsgNextScreen("Pulsa una tecla para continuar.");
        }


        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}