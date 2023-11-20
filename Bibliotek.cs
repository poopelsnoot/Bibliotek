using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    public class Bibliotek : IBibliotek
    {
        public List<Book> bookList; //lista som håller alla böcker
        public List<Låntagare> låntagareList; //lista som håller info om alla som lånat böcker
        
        public Bibliotek() 
        { 
            bookList = new List<Book>();
            låntagareList = new List<Låntagare>();
        }
        public void LäggTillNyBok() //metod för att lägg till en ny bok i biblioteket
        {
            string titel = "";
            string författare = "";

            Console.WriteLine("Vad är titeln på den nya boken?");
            while (titel == "")
            {
                titel = Console.ReadLine();
            }
            
            Console.WriteLine("Vem är författaren?");
            while (författare == "")
            {
                författare = Console.ReadLine();
            }

            Book newBook = new Book(titel, författare);
            bookList.Add(newBook); //lägg till nya boken i listan med böcker
        }
        public void LånaUtBok() //metod för att låna ut en bok
        {
            bool success = false;
            if (bookList != null) //kolla om det finns tillgängliga böcker kvar att låna ut
            {
                foreach (Book book in bookList)
                {
                    if (book.utlåningsstatus == Utlåningsstatus.tillgänglig) success = true;
                }
            }
            if (success == false)
            {
                Console.WriteLine("Det finns inga tillgängliga böcker just nu.");
                return; //hoppa ur metoden om det inte finns några tillgängliga böcker
            }

            long personnummer = SökLåntagare(); //kolla vem som vill låna bok
            Låntagare låntagare = null;
            foreach (Låntagare person in låntagareList) //jämför alla personnummer som finns med det som skrevs in
            {
                if (person.personnummer == personnummer) låntagare = person;
            }
            
            if (låntagare == null)
            {
                Console.WriteLine("Det finns ingen användare med det personnummret. Registrera ny användare? ja/nej"); //finns ingen användare så registrera en ny eller gå tillbaka till menyn
                while (true)
                {
                    string jaNej = Console.ReadLine().ToLower();
                    if (jaNej == "ja") { låntagare = RegistreraNyLåntagare(personnummer); break; }
                    else if (jaNej == "nej") return;
                    else Console.WriteLine("Svara ja eller nej.");
                }
            }

            if (låntagare.antalLånadeBöcker >= låntagare.lånadeBöcker.Length) //kollar så låntagaren inte lånar för många böcker samtidigt
            { 
                Console.WriteLine($"Låntagaren får inte låna fler än {låntagare.lånadeBöcker.Length} böcker."); return; 
            }

            Console.WriteLine("Vilken bok ska lånas ut?");
            Console.WriteLine("___________________________________________");
            foreach (Book bok in bookList) //skriv ut alla tillgängliga böcker
            {
                if (bok.utlåningsstatus == Utlåningsstatus.tillgänglig) Console.WriteLine($"Titel: {bok.titel} \nFörfattare: {bok.författare}");
                Console.WriteLine();
            }
            Console.WriteLine("___________________________________________");

            Book bokAttLånaUt = null;
            while (true)
            {
                string valdBok = Console.ReadLine(); //låntagaren skriver in vald bok

                foreach (Book bok in bookList)
                {
                    if (bok.titel == valdBok && bok.utlåningsstatus == Utlåningsstatus.tillgänglig) { bokAttLånaUt = bok; break; } //finns boken tillgänglig så lånas den ut
                }
                if (bokAttLånaUt != null) break;
                Console.WriteLine("\nDen boken har vi inte inne, testa en annan.");
            }

            låntagare.lånadeBöcker[låntagare.antalLånadeBöcker] = bokAttLånaUt; //lägg till boken i låntagarens boklista
            låntagare.antalLånadeBöcker++; //öka antal lånade böcker för låntagaren
            bokAttLånaUt.bokensLåntagare = låntagare; //lägg till vem som lånar boken
            bokAttLånaUt.utlåningsstatus = Utlåningsstatus.utlånad; //ändra bokens utlåningsstatus till utlånad
            Console.WriteLine($"{bokAttLånaUt.titel} lånas nu ut.");
        }
        public void ÅterlämnaBok() //metod för att lämna tillbaka en utlånad bok
        {
            long personnummer = SökLåntagare(); //välj vilken låntagare som ska lämna tillbaka en bok
            Låntagare låntagare = null;
            foreach (Låntagare person in låntagareList) //jämför alla personnummer som finns med det som skrevs in
            {
                if (person.personnummer == personnummer) låntagare = person;
            }
            if (låntagare == null) //finns ingen användare, gå tillbaka till menyn
            {
                Console.WriteLine("Det finns ingen användare med det personnummret.");
                return; 
            }

            //kolla hur många böcker låntagaren har
            int index = 0;
            int count = 0;
            for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
            {
                if (låntagare.lånadeBöcker[i] != null) { count++; index = i; } 
            }
            if (count == 0) { Console.WriteLine("Användaren har inga lånade böcker."); return; } //har låntagaren inga böcker, gå tillbaka till menyn
            if (count == 1) { ÄndraÅterlämnadBokInfo(låntagare, index); return; } //om det bara finns 1 bok kommer den lämnas tillbaka

            Console.WriteLine("Vilken bok ska lämnas tillbaka?");
            Console.WriteLine("___________________________________________");

            for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
            {
                if (låntagare.lånadeBöcker[i] != null) //finns fler än 1 bok kommer alla skrivas ut och låntagaren får välja vilken som ska lämnas tillbaka
                {
                    Console.WriteLine($"Titel: {låntagare.lånadeBöcker[i].titel} \nFörfattare: {låntagare.lånadeBöcker[i].författare}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("___________________________________________");

            while (true)
            {
                string valdBok = Console.ReadLine(); //låntagaren skriver in vilken bok som ska återlämnas

                for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
                {
                    if (låntagare.lånadeBöcker[i] != null && låntagare.lånadeBöcker[i].titel == valdBok) //om titeln stämmer överens med en av låntagarens böcker så återlämnas den
                    {
                        ÄndraÅterlämnadBokInfo(låntagare, i);
                        return;
                    }
                }
                Console.WriteLine("\nDet är inte en av dina lånade böcker, testa en annan.");
            }
        }
        public void VisaBöcker() //metod för att visa info om alla böcker
        {
            if (bookList == null || bookList.Count == 0) { Console.WriteLine("Det finns inga böcker i biblioteket än."); return; }

            foreach (Book bok in bookList)
            {
                if (bok.utlåningsstatus == Utlåningsstatus.tillgänglig)
                {
                    Console.WriteLine($"Titel: {bok.titel} \nFörfattare: {bok.författare} \nStatus: {bok.utlåningsstatus}");
                    Console.WriteLine();
                }
                else //om boken är utlånad skrivs även info om låntagaren ut
                {
                    Console.WriteLine($"Titel: {bok.titel} \nFörfattare: {bok.författare} \nStatus: {bok.utlåningsstatus} \nLånas av: {bok.bokensLåntagare.namn}, {bok.bokensLåntagare.personnummer}");
                    Console.WriteLine();
                }
            }
        }
        public void VisaLåntagare() //metod för att visa alla låntagare och deras böcker
        {
            if (låntagareList.Count == 0) { Console.WriteLine("Det finns inga låntagare än."); return; }

            foreach (Låntagare _låntagare in låntagareList) //skriv ut info om alla låntagare
            {
                int index = 1;
                Console.WriteLine($"Namn: {_låntagare.namn} \nPersonnummer: {_låntagare.personnummer}");
                for (int i = 0; i < _låntagare.lånadeBöcker.Length; i++) 
                {
                    if (_låntagare.lånadeBöcker[i] != null) //skriv ut alla böcker en låntagare lånar
                    {
                        Console.WriteLine($"Bok {index}: {_låntagare.lånadeBöcker[i].titel}, av {_låntagare.lånadeBöcker[i].författare}");
                        index++;
                    }
                }
                Console.WriteLine();
            }
        }
        private long SökLåntagare() //metod för att kolla om låntagaren redan finns i systemet
        {
            long personnummer;

            Console.WriteLine("Skriv in låntagarens personnummer, YYMMDDXXXX");
            while (true)
            {
                bool success = long.TryParse(Console.ReadLine(), out personnummer); //låntagaren skriver in personnummer
                Console.WriteLine();
                if (success && personnummer.ToString().Count() == 10) return personnummer;
                else Console.WriteLine("Skriv in låntagarens personnummer, YYMMDDXXXX");
            }
        }
        private Låntagare RegistreraNyLåntagare(long personnummer) //metod som registrerar en ny låntagare
        {
            Console.WriteLine("Ange namn:"); 
            string name = "";
            while (name == "")
            {
                name = Console.ReadLine();
            }
            Låntagare newLåntagare = new Låntagare(name, personnummer);
            låntagareList.Add(newLåntagare); //lägg till nya låntagaren i listan med alla låntagare

            return newLåntagare;
        }
        private void ÄndraÅterlämnadBokInfo(Låntagare låntagare, int index)
        {
            Book bokNamn = låntagare.lånadeBöcker[index];

            foreach (Book b in bookList)
            {
                if (b.titel == bokNamn.titel)
                {
                    b.bokensLåntagare = null; //sätt bokens låntagare till null
                    b.utlåningsstatus = Utlåningsstatus.tillgänglig; //ändra bokens utlåningsstatus till tillgänglig
                    Console.WriteLine($"{b.titel} är återlämnad.\n");
                    break;
                }
            }
            låntagare.lånadeBöcker[index] = null; //ta bort boken från indexet
            låntagare.antalLånadeBöcker--; //ta bort 1 från låntagarens antal lånade böcker
        }
        
    }
}
