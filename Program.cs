namespace Bibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bibliotek bibblan = new Bibliotek();
            Låntagare låntagare;
            Book bok;
            long personnummer;
            while (true)
            {
                Console.WriteLine("1. Lägg till ny bok");
                Console.WriteLine("2. Låna ut bok");
                Console.WriteLine("3. Återlämna bok");
                Console.WriteLine("4. Visa böcker");
                Console.WriteLine("5. Visa låntagare");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        bibblan.LäggTillNyBok();
                        break;
                    case 2:
                        Console.WriteLine("Vad är låntagarens personnummer?");
                        personnummer = long.Parse(Console.ReadLine());
                        låntagare = bibblan.RegistreraLåntagare(personnummer);
                        bok = bibblan.BokAttLånaUt();
                        bibblan.LånaUtBok(låntagare, bok);
                        break;
                    case 3:
                        Console.WriteLine("Vad är låntagarens personnummer?");
                        personnummer = long.Parse(Console.ReadLine());
                        låntagare = bibblan.RegistreraLåntagare(personnummer);

                        //kolla så att låntagaren har någon bok att lämna tillbaka
                        int count = 0;
                        for (int i = 0; i < låntagare.lånadeBöcker.Length; i++)
                        {
                            if (låntagare.lånadeBöcker[i] != null) count++; 
                        }
                        if (count == 0)
                        {
                            Console.WriteLine("Du har inte lånat några böcker.");
                            break;
                        }

                        //om låntagaren har lånat böcker
                        bok = bibblan.BokAttLämnaTillbaka(låntagare);
                        bibblan.ÅterlämnaBok(låntagare, bok);
                        break;
                    case 4:
                        bibblan.VisaBöcker();
                        break;
                    case 5:
                        bibblan.VisaLåntagare();
                        break;
                    default:
                        Console.WriteLine("Välj ett alternativ från menyn");
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTryck på valfri knapp för att komma tillbaka till menyn.");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}