using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Customer : Person
    {
        //ATRIBUTOS

        private int? _fidelity_card;

        //CONSTRUCTOR

        public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
        {
            _fidelity_card = fidelityCard;
        }


    }
}
