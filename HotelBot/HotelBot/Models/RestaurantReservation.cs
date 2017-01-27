using Microsoft.Bot.Builder.FormFlow;
using System;

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
        [Prompt("What {&} would you want to go (you can text me a name or a number) {||}")]
        public RestaurantOptions Restaurant;

        [Prompt("For how many people?")]
        public int NumberOfPeople;

        [Prompt("When do you want to go (example 1/29/2017 7:30PM)?")]
        public DateTime DateAndTime;

        public static IForm<RestaurantReservation> BuildForm()
        {
            return new FormBuilder<RestaurantReservation>()
                .Field(nameof(Restaurant))
                .Field(nameof(NumberOfPeople),
                            validate: async (state, value) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = value };
                                var people = int.Parse(value.ToString());
                                if (people > 10)
                                {
                                    result.Feedback = "For more than ten people, please call ext 0 from your cabin";
                                    result.IsValid = false;
                                }
                                return result;
                            })
                .Field(nameof(DateAndTime),
                            validate: async (state, value) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = value };
                                var resDate = DateTime.Parse(value.ToString());
                                if (resDate < DateTime.Now)
                                {
                                    result.Feedback = "Your reservation is in the past.";
                                    result.IsValid = false;
                                }
                                return result;
                            })
                 .Confirm(async (state) =>
                 {
                     var cost = state.NumberOfPeople * 35;
                     return new PromptAttribute($"Your total is {cost:C2} ($35 per person). for {state.Restaurant} for {state.NumberOfPeople} people at {state.DateAndTime}. Is this correct?");
                 })
                .Message("Thank you, your reservation is confirmed")
                .AddRemainingFields()
                .Build();
        }
    }
}