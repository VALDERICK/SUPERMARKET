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
        public int YearsOfService;
        public int Points;
        #endregion

        #region CONSTRUCTOR 
        public Cashier(DateTime hire_date)
        {
            _joiningDate = hire_date;
            Points = 0;
        }
        #endregion

        #region METHODS
        public override void AddPoints(int pointsToAdd)
        {
            Points = pointsToAdd * YearsOfService + 1;
        }
        #endregion

        #region PROPERTY
        public override double GetRating{
            get
            {
               
                int años = DateTime.Now.Year - _joiningDate.Year;

               
                if (_joiningDate > DateTime.Now)
                {
                    años--;
                }

                return años;
            }
        }
        
        #endregion
    }
}
