namespace SUPERMARKET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //TEST ITEM

            Console.WriteLine("--------------------------------ITEMS------------------------------");
            Item item1 = new Item(1, "Manzana",true, 1.50, Category.FRUITS, Packaging.Unit, 50, 10);
            Item item2 = new Item(2, "Entrecot",false, 7.75, Category.MEAT, Packaging.Kg, 10, 20);

            //TEST PERSON 
            Console.WriteLine("--------------------------------PERSONS------------------------------");
            Person cashier1 = new Cashier("91827364A", "Ferran Chic", new DateTime(2015, 4, 13));
            Console.WriteLine(cashier1);

            Person customer1 = new Customer("98765432B", "Victor Granados", 73692827);
            Console.WriteLine(customer1);

            //TEST CASHIER
            Console.WriteLine("--------------------------------CASHIERS------------------------------");
            Cashier cashier2 = new Cashier("12345678A", "Artur Juve", new DateTime(2014, 03, 24));
            Console.WriteLine(cashier2);

            cashier2.AddPoints(150);
            Console.WriteLine($"+points: {cashier2}");

            //TEST CUSTOMER 
            Console.WriteLine("--------------------------------CUSTOMERS------------------------------");
            Customer customer2 = new Customer("98765432B", "Yaryna Blanco", 9872432);
            Console.WriteLine(customer2);

            customer2.AddPoints(50);
            Console.WriteLine($"+points: {customer2}");

            customer2.AddInvoiceAmount(100.50);
            Console.WriteLine($"+invoice: {customer2}");

            //TEST SUPERMARKET 
            Console.WriteLine("--------------------------------SUPERMARKET------------------------------");


            Supermarket supermarket = new Supermarket("BonPreu", "Calle Montilivi");

            supermarket.LoadCustomers("CUSTOMERS.TXT");
            supermarket.LoadCashiers("CASHIERS.TXT");
            supermarket.LoadWarehouse("GROCERIES.TXT");

            Console.WriteLine($"Nom: {supermarket.Name}");
            Console.WriteLine($"Adreça: {supermarket.Address}");
            Console.WriteLine($"Numero de linies obertes: {supermarket.ActiveLines}");

            supermarket.ActiveLines = 3;
            Console.WriteLine($"(+linies)Numero de linies obertes: {supermarket.ActiveLines}");



            //TEST ORDENACIO
            Console.WriteLine("--------------------------------ITEMS ORDENATS------------------------------");

            /*SortedSet<Item> itemsByStock = supermarket.GetItemByStock();
            itemsByStock.Add(new Item(1, "Peix",true, 10.50, Category.FROZEN, Packaging.Unit, 20, 5));
            itemsByStock.Add(new Item(2, "Platano",false,8.75, Category.FRUITS, Packaging.Unit, 15, 3));
            itemsByStock.Add(new Item(3, "Pan",true, 5.25, Category.BREAD, Packaging.Unit, 25, 10));
            itemsByStock.Add(new Item(4, "Pepino",false, 12.30, Category.VEGETABLES, Packaging.Unit, 18, 6));
         
            Console.WriteLine("Elementos ordenados por stock:");
            foreach (Item item in itemsByStock)
            {
               Console.WriteLine(item.ToString());
            }*/



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
            //Console.Clear();
            //Random random = new Random();

            //Person inactivePerson = super.GetAvailableCustomer();

            //if (inactivePerson is Customer)
            //{
            //    Customer inactiveCustomer = (Customer)inactivePerson;

            //    int randomIndex = random.Next(carros.Count);

            //    ShoppingCart randomCart = carros.AddOneItem(randomIndex);

            //    // Seleccionar un artículo del supermercado al azar
            //    int randomItemId = random.Next(1, super.Warehouse.Count + 1);
            //    Item randomWarehouseItem = super.Warehouse[randomItemId];

            //    // Agregar el artículo seleccionado al carro de la compra
            //    double quantity = random.Next(1, 6);
            //    randomCart.AddOne(randomWarehouseItem, quantity);

            //    // Mostrar el carro de la compra antes y después de agregar el artículo
            //    Console.WriteLine($"Carro de la compra seleccionado antes de agregar el artículo:\n{randomCart.ToString()}\n");
            //    // Mostrar carro después de agregar el artículo
            //    Console.WriteLine($"Carro de la compra seleccionado después de agregar el artículo:\n{randomCart.ToString()}\n");

            //    Console.WriteLine("Se ha agregado un artículo aleatorio al carro de la compra seleccionado.");
            //}
            //else
            //{
            //    Console.WriteLine("No hay clientes inactivos disponibles para asignar un nuevo carro de la compra o el cliente no es del tipo Customer.");
            //}

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
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
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
            bool fet = true;
            Console.Clear();
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
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
            bool fet = true;
            MsgNextScreen("PREM UNA TECLA PER ANAR AL MENÚ PRINCIPAL");
            return fet;
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

            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

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
            Console.WriteLine("CARROS VOLTANT PEL SUPER (PENDENTS D'ANAR A PAGAR): ");
            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");

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



            MsgNextScreen("PREM UNA TECLA PER CONTINUAR");
        }


        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }

    }
}