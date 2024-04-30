﻿namespace SUPERMARKET
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

                int years = DateTime.Now.Year - _joiningDate.Year / 365;


                if (_joiningDate > DateTime.Now)
                {
                    years--;
                }

                return years;
            }
        }
        #endregion

        #region PROPERTY
        public override double GetRating
        {


            int antigitat = YearsOfService;


            double totalFacturado = _totalInvoice;

            get { 
            int antigitat = YearsOfService;

            
            double totalFacturado = _totalInvoiced; 


            double total10 = 0.1 * totalFacturado;

            double rating = antigitat * 365 + total10;

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
            sb.Append(YearsOfService.ToString());
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


            return base.ToString();

        }

    }
}
