using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using HotelBot.Models;

namespace HotelBot.Domain
{
    public class CruiseSearchApi
    {
        private static List<string> ports = new List<string> { "LAX", "MIA", "FLL", "TPA" };
        private static List<string> destinations = new List<string> { "A", "C", "BH", "E" };
        private static List<string> months = new List<string> { "01", "02", "03", "04", "05","06","07","08","09","10","11","12" };
        private static List<string> years = new List<string> { "2016", "2017", "2018", "2019" };

        public static List<string> Search(CruiseSearch state)
        {
            var destination = destinations[(int)state.Destination];
            var port = ports[(int)state.Port];
            var numAdults = (int)state.Guests;
            var date = months[(int)state.Month] + years[(int)state.Year];

            string url =
                String.Format(
                    "http://www.carnival.com/bookingengine/api/search?exclDetails=false&layout=grid&numAdults={0}&datFrom={1}&datTo={1}&dest={2}&port={3}&numChildren=0&pageNumber=1&pageSize=8&showBest=true&sort=FromPrice&useSuggestions=true",
                    numAdults, date, destination, port);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                var results = (new JavaScriptSerializer()).Deserialize<SearchResults>(json);
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                var minPrice = results.Results.Itineraries.Select(a => a.LeadSailing).Min(a => a.FromPrice);
                var itin = results.Results.Itineraries.Where(a => a.LeadSailing.FromPrice == minPrice).First();
                var msg = String.Format("We found a cruise with cheapest rate of {0:C} on {1}", itin.LeadSailing.FromPrice, String.Format("{0:dd MMM yyyy}", itin.LeadSailing.DepartureDate));
                List<string> msgs = new List<string>();
                msgs.Add(msg);
                var msg1 = String.Format(
                    "https://www.carnival.com/cruise-deals/cruise-deal-finder.aspx?numAdults={0}&datFrom={1}&datTo={1}&dest={2}&port={3}",
                    numAdults, date, destination, port);
                msgs.Add(msg1);
              
                return msgs;
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