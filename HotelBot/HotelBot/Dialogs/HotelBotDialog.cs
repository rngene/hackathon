using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using HotelBot.Models;

namespace HotelBot.Dialogs
{
    public class HotelBotDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuation);
            }),
            new RegexCase<IDialog<string>>(new Regex("reservation|restaurant|dinner", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(RestaurantReservation.BuildForm, FormOptions.PromptInStart), AfterRestaurantConfirmation);
            }),
            new RegexCase<IDialog<string>>(new Regex("service|room dinning|room service", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(RoomService.BuildForm, FormOptions.PromptInStart), AfterRestaurantConfirmation);
            }),
            new RegexCase<IDialog<string>>(new Regex("child|care", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(ChildCare.BuildForm, FormOptions.PromptInStart), AfterRestaurantConfirmation);
            }),
            new DefaultCase<string, IDialog<string>>((context, text) =>
            {
                return Chain.ContinueWith(new DontUnderstandDialog(), AfterGreetingContinuation);
            }))
            .Unwrap()
            .PostToUser();
           
        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            return Chain.Return("How can I help you today?  (restaurant reservations, room service or child care) ");
        }

        private async static Task<IDialog<string>> AfterRestaurantConfirmation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }
    }
}