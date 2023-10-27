using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Låntagare  
    {
        private string namn;
        private long personnummer;
        public Book[] lånadeBöcker;
        public int antalLånadeBöcker { get; set; }

        public Låntagare(string namn, long personnummer) 
        {
            this.namn = namn;
            this.personnummer = personnummer;
            lånadeBöcker = new Book[5]; //en låntagare får som mest låna 5 böcker samtidigt
            antalLånadeBöcker = 0;
        }

        public string Namn { get { return namn; } }
        public long Personnummer { get { return personnummer; } }
    }
}

