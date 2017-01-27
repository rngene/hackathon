﻿using HotelBot.Domain;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    public enum Destination
    {
        NewEngland,
        Alaska,
        Caribbean,
        Bahamas,
        Europe
    }


    public enum Port
    {
        LosAngeles,
        Miami,
        FortLauderdale,
        Tampa,
        Seattle
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

    public enum NumGuests
    {
        one,two,three,four,five
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
        public Month Month;

        public static IForm<CruiseSearch> BuildForm()
        {
            OnCompletionAsyncDelegate<CruiseSearch> processOrder = async (context, state) =>
            {
                var srchMsg = CruiseSearchApi.Search();
                var msg = context.MakeMessage();
                msg.Text = srchMsg;
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