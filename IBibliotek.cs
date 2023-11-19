using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    internal interface IBibliotek
    {
        void LäggTillNyBok();
        void LånaUtBok();
        void ÅterlämnaBok();
        void VisaBöcker();
        void VisaLåntagare();
    }
}
