using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    public enum Beverages
    {
        Nothing,
        Tea,
        Coffee,
        OrangeJuice,
        AppleJuice
    }

    public enum Salads
    {
        VeganSalad,
        GardenSalad,
        CaesarSalad,
        GreekSalad
    }

    public enum BreakfastBites
    {
        SesameBagel,
        PlainBagelWithCreamCheese,
        Croissant,
        CheeriosWithMilk,
        Oatmeal
    }

    public enum Beers
    {
        Budlight,
        SamuelAdams,
        Corona,
        Budweiser,
        Heikenen
    }


    [Serializable]
    public class RoomService
    {
        public List<BreakfastBites> BreakfastBites;
        public Salads Salads;
        public List<BreakfastBites> Beverages;
        public Beers Beer;

        public static IForm<RoomService> BuildForm()
        {
            return new FormBuilder<RoomService>()
                .Build();
        }
    }
}