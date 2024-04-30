using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Customer : Person
    {
        //ATRIBUTOS

        private int? _fidelity_card;

        //PROPIEDADES

        public override double GetRating
        {
            get { return _totalInvoiced * 0.02; } 
        }

        //CONSTRUCTOR

        public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
        {
            _fidelity_card = fidelityCard;
        }

        //METODOS

        public override void AddPoints(int pointsToAdd)
        {
            
            if (_fidelity_card != null)
            {
                _points += pointsToAdd;
            }
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DNI/NIE->").Append(Id).Append(" NOM->").Append(FullName).Append(" RATING->")
              .Append(GetRating.ToString("F2")).Append(" VENDES->").Append(_totalInvoiced.ToString("F1"))
              .Append(" € PUNTS->").Append(_points).Append(" DISPONIBLE->");

            if (active)
            {
                sb.Append("N");
            }
            else
            {
                sb.Append("S");
            }

            return sb.ToString();
        }
    }


}
}
