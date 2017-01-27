using HotelBot.Domain;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    public enum Destination
    {
        Alaska,
        Caribbean,
        Bahamas,
        Europe
    }


    public enum Port
    {
        LosAngeles=0,
        Miami=1,
        FortLauderdale=2,
        Tampa=3
    }

    public enum Month
    {
        Jan,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec
    }

    public enum Year
    {
        _2016,
        _2017,
        _2018,
        _2019
    }

    public enum NumGuests
    {
        one=1,two=2,three=3,four=4,five=5
    }

    [Serializable]
    public class CruiseSearch
    {
        [Prompt("What {&} would you like to sail from (you can tell me a name or a number? {||}",ChoiceFormat = "{1}")]
        public Port Port;

        [Prompt("What {&} would you like to go to (you can tell me a name or a number? {||}",ChoiceFormat = "{1}")]
        public Destination Destination;

        [Prompt("For how many people? {||}", ChoiceFormat = "{1}")]
        public NumGuests Guests;

        [Prompt("Which {&} would you like to travel? {||}", ChoiceFormat = "{1}")]
        public Year Year;

        [Prompt("Which {&} would you like to travel? {||}", ChoiceFormat = "{1}")]
        public Month Month;

        public static IForm<CruiseSearch> BuildForm()
        {
            OnCompletionAsyncDelegate<CruiseSearch> processOrder = async (context, state) =>
            {
                var srchMsgs = CruiseSearchApi.Search(state);
                var msg = context.MakeMessage();
                msg.Text = srchMsgs[0];
                await context.PostAsync(msg);
                msg = context.MakeMessage();
                msg.Text = srchMsgs[1];
                await context.PostAsync(msg);
            };

            return new FormBuilder<CruiseSearch>().OnCompletion(processOrder)
                //.Field(nameof(Port))
                //.Field(nameof(Destination))
                //.Field(nameof(NumberOfPeople),
                //            validate: async (state, value) =>
                //            { = 
                //                var result = new ValidateResult { IsValid = true };
                //                //var values = ((int)value);
                //                //value.ToString();
                //                var people = int.Parse(value.ToString());
                //                if (people >= 8)
                //                {
                //                    result.Feedback = "Too many people!";
                //                    result.IsValid = false;
                //                }
                //                return result;
                //            })
                //.AddRemainingFields()
                .Build();
        }
    }
}