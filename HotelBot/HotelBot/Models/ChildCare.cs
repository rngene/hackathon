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

        public DateTime DropOff;
        public int NumberOfHours;
        public string SpecialRequest;

        public static IForm<ChildCare> BuildForm()
        {
            return new FormBuilder<ChildCare>()
                .Build();
        }
    }


}