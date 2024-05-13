using System.Text;

namespace SUPERMARKET
{
    public class Cashier : Person
    {
        #region ATRIBUTES
        private DateTime _joiningDate;
        

        #endregion

        #region CONSTRUCTOR 
        public Cashier(string id, string fullName, DateTime contracte) : base(id, fullName)
        {
            _joiningDate = contracte;
            
        }
        #endregion

        #region METHODS
        public override void AddPoints(int pointsToAdd)
        {
            _points += (pointsToAdd * (YearsOfService) + 1); ;
            
        }

        public int YearsOfService
        {
            get
            {
                DateTime ahora = DateTime.Now;

                TimeSpan antiguedad = ahora - _joiningDate;


                int años = antiguedad.Days / 365;

                return años;

            }

        }
        #endregion

        #region PROPERTY
        public override double GetRating
        {
            get {
                DateTime ahora = DateTime.Now;
                TimeSpan antiguedad = ahora - _joiningDate;

                int años = (int)(antiguedad.TotalDays);

                double rating = años + 0.1 * _points;

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
