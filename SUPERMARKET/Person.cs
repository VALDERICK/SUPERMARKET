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
        protected bool active = false;

        //PROPIEDADES
    
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public string Id { get => _id; }
        public string FullName { get => _fullName; }

        //PROPIEDADES ABSTRACTAS
        public abstract double GetRating {get;  }
   

        // CONSTRUCTORES

        protected Person(string id, string fullName, int points)
        {
            Id = id;
            FullName = fullName;
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
