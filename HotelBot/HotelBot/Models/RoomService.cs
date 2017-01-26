using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    [Serializable]
    public class RoomService
    {
        public string FoodRequest;

        public static IForm<RoomService> BuildForm()
        {
            return new FormBuilder<RoomService>()
                .Build();
        }
    }
}