using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    public enum BedSizeOptions
    {
        King,
        Queen,
        Single,
        Double
    }

    public enum RestaurantOptions
    {
        CucinaDelCapitano,
        Fahrenheit555,
        ChefsTable,
        GreenEggsAndHam,
        JiJiAsianKitchen,
        BonsaiSushi
    }

    [Serializable]
    public class RestaurantReservation
    {
        public RestaurantOptions Restaurant;
        public int NumberOfPeople;
        public DateTime DateAndTime;
        
        public static IForm<RestaurantReservation> BuildForm()
        {
            return new FormBuilder<RestaurantReservation>()
                .Build();
        }
    }
}