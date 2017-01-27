using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace HotelBot.Domain
{
    public class CruiseSearchApi
    {
        public static string Search()
        {

            string url =
                String.Format(
                    "http://www.carnival.com/bookingengine/api/search?exclDetails=false&layout=grid&numAdults=2&numChildren=0&pageNumber=1&pageSize=8&showBest=true&sort=FromPrice&useSuggestions=true");
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                var results = (new JavaScriptSerializer()).Deserialize<SearchResults>(json);
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                var minPrice = results.Results.Itineraries.Select(a => a.LeadSailing).Min(a => a.FromPrice);
                var itin = results.Results.Itineraries.Where(a => a.LeadSailing.FromPrice == minPrice).First();
                var msg = String.Format("We found a cruise with cheapest rate of {0} on {1}", itin.LeadSailing.FromPrice, itin.LeadSailing.DepartureDate);
                return msg;
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
            public LeadSailing LeadSailing { get; set; }
        }

        public class LeadSailing
        {
            public decimal FromPrice { get; set; }
            public DateTime ArrivalDate { get; set; }
            public DateTime DepartureDate { get; set; }
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