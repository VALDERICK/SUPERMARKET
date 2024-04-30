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

        public Cashier(string id, string fullName, int points, DateTime contracte) : base(id, fullName, points)
        {
            _joiningDate = contracte;
        }
        #endregion

        #region CONSTRUCTOR 

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
                TimeSpan tiempoDeServicio = DateTime.Now - _joiningDate;

                int años = Convert.ToInt32(tiempoDeServicio.Days / 365);


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
