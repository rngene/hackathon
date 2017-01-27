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
        [Prompt("What {&} would you want to go (you can tell me a name or a number? {||}")]
        public RestaurantOptions Restaurant;

        [Prompt("For how many people?")]
        public int NumberOfPeople;

        [Prompt("When do you want to go (example 2/2/2016 5:30PM)?")]
        public DateTime DateAndTime;

        public static IForm<RestaurantReservation> BuildForm()
        {
            return new FormBuilder<RestaurantReservation>()
                .Field(nameof(Restaurant))
                .Field(nameof(NumberOfPeople),
                            validate: async (state, value) =>
                            {
                                var result = new ValidateResult { IsValid = true };
                                //var values = ((int)value);
                                //value.ToString();
                                var people = int.Parse(value.ToString());
                                if (people >= 8)
                                {
                                    result.Feedback = "Too many people!";
                                    result.IsValid = false;
                                }
                                return result;
                            })
                .Field(nameof(DateAndTime))
                .AddRemainingFields()
                .Build();
        }
    }
}