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
        private DateTime _joibingDate;
        public int YearsOfService;
        public int Points;
        #endregion

        #region METHODS
        public void AddPoints(int pointsToAdd)
        {
            Points= pointsToAdd * YearsOfService + 1;

        }
        #endregion

        #region PROPERTY
        public override double GetRating{
            get { }
        }
        
        #endregion
    }
}
