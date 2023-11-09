using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Book 
    {
        public string titel { get; set; }
        public string författare { get; set; }
        public Låntagare? bokensLåntagare { get; set; } //om boken är utlånad kommer den ha en låntagare, annars är låntagare null
        public Utlåningsstatus utlåningsstatus { get; set; }

        public Book(string titel, string författare)
        {
            utlåningsstatus = Utlåningsstatus.tillgänglig; //nya böcker startar alltid som tillgängliga
            this.titel = titel;
            this.författare = författare;
        }
    }
}
