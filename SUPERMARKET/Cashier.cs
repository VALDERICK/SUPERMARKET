using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Cashier : Person
    {
        #region ATRIBUTES
        private DateTime _joiningDate;
        public int Points;

        #endregion
       
        #region CONSTRUCTOR 
        public Cashier(string id, string fullName, int points, DateTime contracte) : base(id, fullName, points)
        {
            _joiningDate = contracte;
        }
        #endregion

        #region METHODS
        public override void AddPoints(int pointsToAdd)
        {
            Points = pointsToAdd * YearsOfService + 1;
        }

        public int YearsOfService
        {
            get
            {
                DateTime ahora = DateTime.Now;

                    DateTime antiguedad = new DateTime(ahora.Year, _joiningDate.Month, _joiningDate.Day);
                    int años = ahora.Year - _joiningDate.Year;
                DateTime antiguedad = new DateTime(ahora.Year, _joiningDate.Month, _joiningDate.Day);
                int años = ahora.Year - _joiningDate.Year;


                if (antiguedad > ahora)
                {
                    return 0;
                }


                if (antiguedad <= ahora)
                {
                    años++;
                }

                return años;
            }
        }
        #endregion

        #region PROPERTY
        public override double GetRating
        {
            get { 
            int antigitat = YearsOfService;

            
            double totalFacturado = _totalInvoiced; 

            double total10 = 0.1 * totalFacturado;

            double rating = antigitat*365 + total10;

            return rating;
            }
        }
        #endregion

        public override string ToString()
        { 
            StringBuilder sb = new StringBuilder();
            sb.Append($"DNI/NIE->{Id}  NOM->{FullName}  RATING-> ");
            sb.Append(GetRating.ToString());
            sb.Append(" ANTIGUITAT->").Append(YearsOfService);
            sb.Append(" VENDES->").Append(_totalInvoiced.ToString());
            sb.Append(" € PUNTS->").Append(_points).Append(" DISPONIBLE->");

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
