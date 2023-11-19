using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Book 
    {
        public string titel;
        public string författare;
        public Låntagare? bokensLåntagare; //om boken är utlånad kommer den ha en låntagare, annars är låntagare null
        public Utlåningsstatus utlåningsstatus;

        public Book(string titel, string författare)
        {
            utlåningsstatus = Utlåningsstatus.tillgänglig; //nya böcker startar alltid som tillgängliga
            this.titel = titel;
            this.författare = författare;
        }
    }
}
