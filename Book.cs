using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Book 
    {
        private string titel;
        private string författare;
        public Låntagare? låntagare { get; set; }
        public Utlåningsstatus utlåningsstatus { get; set; }

        public Book(string titel, string författare)
        {
            utlåningsstatus = Utlåningsstatus.tillgänglig;
            this.titel = titel;
            this.författare = författare;
        }

        public string Titel { get { return titel; } }
        public string Författare { get { return författare;} }
    }
}
