using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using HotelBot.Models;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.FormFlow;

namespace HotelBot.Dialogs
{
    [LuisModel("ffbd0d63-e99c-44c6-a1da-6c51de8214fc", "4dcfedf2b3154301a609ddb6c6ed1cea")]
    [Serializable]
    public class MyLuisDialog : LuisDialog<RestaurantReservation>
    {
        private readonly BuildFormDelegate<RestaurantReservation> RestaurantReservationDelegate;

        public MyLuisDialog(BuildFormDelegate<RestaurantReservation> restaurantReservation)
        {
            this.RestaurantReservationDelegate = restaurantReservation;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Hi")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("RestaurantReservation")]
        public async Task RestaurantReservation(IDialogContext context, LuisResult result)
        {
            var enrollmentForm = new FormDialog<RestaurantReservation>(new RestaurantReservation(), this.RestaurantReservationDelegate, FormOptions.PromptInStart);
            context.Call<RestaurantReservation>(enrollmentForm, Callback);
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}