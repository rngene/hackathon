using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    [Serializable]
    public class ChildCare
    {
        [Prompt("What's the date and time of your drop off (example 1/29/2017 10:00PM)?")]
        public DateTime DateAndTime;

        [Prompt("For how many children?")]
        public int NumberOfChildren;

        [Prompt("For how many hours you need baby sitting?")]
        public int NumberOfHours;

        public static IForm<ChildCare> BuildForm()
        {
            return new FormBuilder<ChildCare>()
                .Message("Night Owl Child Care is available from 10 pm to 1 AM for children 2 to 11 years old")
                .Field(nameof(DateAndTime),
                 validate: async (state, value) =>
                 {
                     var result = new ValidateResult { IsValid = true, Value = value };
                     var resDate = DateTime.Parse(value.ToString());
                     if (resDate < DateTime.Now)
                     {
                         result.Feedback = "Your drop off is in the past.";
                         result.IsValid = false;
                     }
                     return result;
                 })
                 .AddRemainingFields()
                 .Confirm(async (state) =>
                 {
                     var cost = state.NumberOfChildren * state.NumberOfHours * 6.75 * 1.15;
                     return new PromptAttribute($"Your total is {cost:C2} ($6.75 X hour X kid + 15% tip) for babysitting  {state.NumberOfChildren} children for {state.NumberOfHours} hours at {state.DateAndTime}. Is this correct?");
                 })
                .Message("Thank you, your babysitting reservation is confirmed")
                .Build();
        } }
    }


