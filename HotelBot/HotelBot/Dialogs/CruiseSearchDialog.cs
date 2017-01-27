using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading.Tasks;
using HotelBot.Models;
using Twilio;

namespace HotelBot.Dialogs
{
    public class CruiseSearchDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
             .Switch(
                        new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
                        {
                            return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuation);
                        }),
                        new DefaultCase<string, IDialog<string>>((context, text) =>
                        {
                            return Chain.ContinueWith(FormDialog.FromForm(CruiseSearch.BuildForm, FormOptions.PromptInStart), AfterChildCare);
                        })
            )
            .Unwrap()
            .PostToUser();
           
        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            //var token = await item;
            //var accountSid = "ACbb85ffd271203038fea0db9c0d06d8c6"; // Your Account SID from www.twilio.com/console
            //var authToken = "39880d052d1a2048f321d0725dc6d6ae";  // Your Auth Token from www.twilio.com/console

            //var twilio = new TwilioRestClient(accountSid, authToken);
            //var message = twilio.SendMessage(
            //    "+17862570432", // From (Replace with your Twilio number)
            //    "7863271555", // To (Replace with your phone number)
            //    "Hello from C#"
            //    );

            return Chain.Return("Where do you to want to travel today?");
        }

        private async static Task<IDialog<string>> AfterRestaurant(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;

            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }

        private async static Task<IDialog<string>> AfterRoomService(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;

            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }

        private async static Task<IDialog<string>> AfterChildCare(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;

            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }
    }
}