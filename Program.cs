namespace Bibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bibliotek bibblan = new Bibliotek(); //skapa ett bibliotek som kommer hantera all information/data
            bool success;
            int choice;

            bibblan.bookList = ExternLagring.UploadBookInfo(); //ladda upp all sparad info när programmet startar
            bibblan.låntagareList = ExternLagring.UploadLåntagareInfo();

            while (true)
            {
                Console.WriteLine("1. Lägg till ny bok");
                Console.WriteLine("2. Låna ut bok");
                Console.WriteLine("3. Återlämna bok");
                Console.WriteLine("4. Visa böcker");
                Console.WriteLine("5. Visa låntagare");
                Console.WriteLine("6. Stäng biblioteket");

                while (true)
                {
                    success = int.TryParse(Console.ReadLine(), out choice); //ser till att användaren väljer ett alternativ som finns, annars får användaren välja nytt
                    if (success && choice < 7 && choice > 0) break;
                    else Console.WriteLine("Välj ett av alternativen från menyn.");
                }
                Console.WriteLine("___________________________________________");
                Console.WriteLine();

                switch (choice)
                {
                    case 1: //lägg till ny bok
                        bibblan.LäggTillNyBok();
                        break;
                    case 2: //låna ut bok
                        bibblan.LånaUtBok();
                        break;
                    case 3: //återlämna bok
                        bibblan.ÅterlämnaBok();
                        break;
                    case 4: //visa böcker
                        bibblan.VisaBöcker();
                        break;
                    case 5: //visa låntagare
                        bibblan.VisaLåntagare();
                        break;
                    case 6: //spara allt i externa filer och avsluta sedan programmet
                        ExternLagring.SaveInfo(bibblan.bookList, bibblan.låntagareList);
                        Console.WriteLine("Biblioteket stänger. Välkommen åter!");
                        return;
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