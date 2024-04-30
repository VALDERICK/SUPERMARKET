using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    internal class Cashier
    {
        #region ATRIBUTES
        private DateTime _joibingDate;
        public int YearsOfService;
        public int Points=0;
        #endregion

        #region METHODS
        public void AddPoints(int pointsToAdd)
        {
            
        }
        #endregion

        #region PROPERTY
        public override double GetRating()
        {
            return null;
        }
        #endregion
    }
}
