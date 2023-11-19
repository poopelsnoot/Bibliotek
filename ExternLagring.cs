using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bibliotek
{
    internal static class ExternLagring
    {
        public static void SaveInfo(List<Book> bookList, List<Låntagare> låntagareList) //metod som sparar all info om böcker och låntagare
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore //när listan som sparas innehåller objekt med properties som också är objekt skapas en loop-error. Denna kod säger åt programmet att ignorera loopen.
            };

            if (File.Exists("saveLåntagare.json")) File.Delete("saveLåntagare.json");
            var jsonLåntagare = JsonConvert.SerializeObject(låntagareList, Formatting.Indented, jsonSettings);
            File.WriteAllText("saveLåntagare.json", jsonLåntagare);

            if (File.Exists("saveBooks.json")) File.Delete("saveBooks.json");
            var jsonBooks = JsonConvert.SerializeObject(bookList, Formatting.Indented, jsonSettings);
            File.WriteAllText("saveBooks.json", jsonBooks);
        }
        public static List<Book> UploadBookInfo() //metod som laddar upp all info om böcker
        {
            if (File.Exists("saveBooks.json"))
            {
                var json = File.ReadAllText("saveBooks.json");
                return JsonConvert.DeserializeObject<List<Book>>(json);
            }

            List<Book> list = new List<Book>();
            return list;
        }
        public static List<Låntagare> UploadLåntagareInfo() //metod som laddar upp all info om låntagare
        {
            if (File.Exists("saveLåntagare.json"))
            {
                var json = File.ReadAllText("saveLåntagare.json");
                return JsonConvert.DeserializeObject<List<Låntagare>>(json);
            }
            return new List<Låntagare>();
        }
    }
}
