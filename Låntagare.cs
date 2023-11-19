using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Låntagare  
    {
        public string namn;
        public long personnummer;
        public Book?[] lånadeBöcker; //array som innehåller de böcker en specifik låntagare lånar
        public int antalLånadeBöcker;

        public Låntagare(string namn, long personnummer) 
        {
            this.namn = namn;
            this.personnummer = personnummer;
            lånadeBöcker = new Book[5]; //en låntagare får som mest låna 5 böcker samtidigt
            antalLånadeBöcker = 0;
        }
    }
}

