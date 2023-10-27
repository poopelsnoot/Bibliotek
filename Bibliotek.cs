using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Bibliotek //ha txt fil att lagra böcker och låntagare i
    {
        List<Book> availableBooks = new List<Book>();
        List<Book> unavailableBooks = new List<Book>();
        List<Låntagare> låntagare = new List<Låntagare>();

        public void LäggTillNyBok() //metod för att lägg till en ny bok i biblioteket
        {
            Console.WriteLine("Vad är titeln på den nya boken?");
            string titel = Console.ReadLine();
            Console.WriteLine("Vem är författaren?");
            string författare = Console.ReadLine();

            Book newBook = new Book(titel, författare);
            availableBooks.Add(newBook);
        }

        public void LånaUtBok(Låntagare låntagare, Book bok) //metod för att låna ut en bok
        {
            if (låntagare.antalLånadeBöcker < låntagare.lånadeBöcker.Length)
            {
                låntagare.lånadeBöcker[låntagare.antalLånadeBöcker] = bok;
                låntagare.antalLånadeBöcker++;
                availableBooks.Remove(bok);
                unavailableBooks.Add(bok);
                bok.låntagare = låntagare;
                bok.utlåningsstatus = Utlåningsstatus.utlånad;
            }
            else { Console.WriteLine($"Låntagaren får inte låna fler än {låntagare.lånadeBöcker.Length} böcker."); }
        }

        public void ÅterlämnaBok(Låntagare låntagare, Book bok) //metod för att lämna tillbaka en utlånad bok
        {
            int index = Array.IndexOf(låntagare.lånadeBöcker, bok);
            låntagare.lånadeBöcker[index] = null;
            låntagare.antalLånadeBöcker--;
            availableBooks.Add(bok);
            unavailableBooks.Remove(bok);
            bok.låntagare = null;
            bok.utlåningsstatus = Utlåningsstatus.tillgänglig;
        }

        public void VisaBöcker() //metod för att visa info om alla tillgängliga och utlånade böcker
        {
            foreach (Book bok in availableBooks)
            {
                Console.WriteLine($"Titel: {bok.Titel} \nFörfattare: {bok.Författare} \nStatus: {bok.utlåningsstatus}");
                Console.WriteLine();
            }
            foreach (Book bok in unavailableBooks)
            {
                Console.WriteLine($"Titel: {bok.Titel} \nFörfattare: {bok.Författare} \nStatus: {bok.utlåningsstatus} \nLånas av: {bok.låntagare.Namn}, {bok.låntagare.Personnummer}");
                Console.WriteLine();
            }
        }

        public void VisaLåntagare() //metod för att visa alla låntagare och deras böcker
        {
            foreach (Låntagare _låntagare in låntagare)
            {
                Console.WriteLine($"Namn: {_låntagare.Namn} \nPersonnummer: {_låntagare.Personnummer}");
                for (int i = 0; i < _låntagare.lånadeBöcker.Length; i++)
                {
                    if (_låntagare.lånadeBöcker[i] != null)
                    {
                        Console.WriteLine($"Bok {i + 1}: {_låntagare.lånadeBöcker[i].Titel}, {_låntagare.lånadeBöcker[i].Författare}");
                    }
                }
                Console.WriteLine();
            }
        }

        public Låntagare RegistreraLåntagare(long personnummer) //metod för att kolla om låntagaren redan finns i systemet. Skapa en ny om låntagaren inte finns
        {
            foreach (Låntagare _låntagare in låntagare)
            {
                if (_låntagare.Personnummer == personnummer) return _låntagare;
            }

            Console.WriteLine("Vad är låntagarens namn?");
            string namn = Console.ReadLine();
            Låntagare newLåntagare = new Låntagare(namn, personnummer);
            låntagare.Add(newLåntagare);

            return newLåntagare;
        }

        public Book BokAttLånaUt() //metod för att välja en tillgänglig bok att låna ut 
        {
            Console.WriteLine("Vilken bok ska lånas ut?");
            foreach (Book bok in availableBooks)
            {
                Console.WriteLine($"{bok.Titel}, {bok.Författare}");
            }
            Console.WriteLine("___________________________________________");
            while (true) //gör try parse felsökning istället?
            {
                string valdBok = Console.ReadLine();

                foreach (Book bok in availableBooks)
                {
                    if (bok.Titel == valdBok) return bok;
                }
                Console.WriteLine("\nDen boken har vi inte, testa en annan.");
            }
        }
        public Book BokAttLämnaTillbaka(Låntagare låntagare) //metod för att välja vilken bok som ska lämnas tillbaka om användaren har lånat fler än 1 bok
        {
            int count = 0;
            int index = 0;
            for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
            {
                if (låntagare.lånadeBöcker[i] != null) { count++; index = i; }
            }
            if (count == 1) return låntagare.lånadeBöcker[index];

            Console.WriteLine("Vilken bok ska lämnas tillbaka?");
            Console.WriteLine("___________________________________________");

            for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
            {
                if (låntagare.lånadeBöcker[i] != null)
                {
                    Console.WriteLine($"{låntagare.lånadeBöcker[i].Titel}, {låntagare.lånadeBöcker[i].Författare}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("___________________________________________");

            while (true) //gör try parse felsökning istället?
            {
                string valdBok = Console.ReadLine();

                for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
                {
                    if (låntagare.lånadeBöcker[i] != null)
                    {
                        if (låntagare.lånadeBöcker[i].Titel == valdBok) return låntagare.lånadeBöcker[i];
                    }
                }
                Console.WriteLine("\nDet är inte en av dina lånade böcker, testa en annan.");
            }
        }
    }
}
