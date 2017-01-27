using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Dialogs
{
    [Serializable]
    public class CruiseSearch
    {
        public string Destination;


        public static IForm<CruiseSearch> BuildForm()
        {
            return new FormBuilder<CruiseSearch>()
                .Build();
        }
    }
}
/**
 *using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CallApi
{
    class Program
    {
        static void Main(string[] args)
        {

            string url =
                String.Format(
                    "http://www.carnival.com/bookingengine/api/search?exclDetails=false&layout=grid&numAdults=2&numChildren=0&pageNumber=1&pageSize=8&showBest=true&sort=FromPrice&useSuggestions=true");
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                var results = (new JavaScriptSerializer()).Deserialize<SearchResults>(json);
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();


            }
        }

        public class SearchResults
        {
            public Result Results { get; set; }
        }

        public class Result
        {
            public List<Itinerary> Itineraries { get; set; }
        }

        public class Itinerary
        {
            public List<Sailing> Sailings { get; set; }
        }

        public class Sailing
        {
            public DateTime ArrivalDate { get; set; }
            public DateTime DepartureDate { get; set; }
            public Rooms Rooms { get; set; }
            public Decimal TaxesAndFeesForLowestPrice { get; set; }
        }

        public class Rooms
        {
            public RoomDetail Interior { get; set; }
            public RoomDetail Oceanview { get; set; }
            public RoomDetail Balcony { get; set; }
            public RoomDetail Suite { get; set; }
        }

        public class RoomDetail
        {
            public Decimal Price { get; set; }
            public bool SoldOut { get; set; }
        }
    }
}

 * */
