using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPERMARKET
{
    public abstract class Person : IComparable<Person>
    {
        //ATRIBUTS

        private string _id;
        private string _fullName;
        protected int _points;
        protected double _totalInvoiced;
        protected bool active =false;
        
        //PROPIEDADES
        public string FullName
        {
            get { return _fullName; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        //PROPIEDADES ABSTRACTAS
        public abstract int GetRating();

        // CONSTRUCTORES

        protected Person(string id, string fullName, int points)
        {
            _id = id;
            _fullName = fullName;
            _points = points;
            _totalInvoiced = 0; 
            active = false;     
        }
        protected Person(string id, string fullName) : this(id, fullName, 0) { }

        //METODOS

        public void AddInvoiceAmount(double amount)
        {
            _totalInvoiced += amount;
        }

        public override string ToString()
        {
            string resposta = "";
            
            if (active) resposta= "DISPONIBLE -> N";           
            else resposta= "DISPONIBLE -> S";
            
            return resposta;
        }

        //METODOS ABSTRACTOS

        public abstract void AddPoints(int pointsToAdd);


        //COMPARETO
        public int CompareTo(Person other)
        {
            if (other == null) return 1;
            return GetRating().CompareTo(other.GetRating());
        }
    }
}
