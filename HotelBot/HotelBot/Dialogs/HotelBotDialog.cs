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
using System.Net;
using System.Web.Script.Serialization;

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
                return Chain.ContinueWith(FormDialog.FromForm(RestaurantReservation.BuildForm, FormOptions.PromptInStart), AfterRestaurant);
            }),
            new RegexCase<IDialog<string>>(new Regex("service|room dinning|room service", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(RoomService.BuildForm, FormOptions.PromptInStart), AfterRoomService);
            }),
            new RegexCase<IDialog<string>>(new Regex("child|care", RegexOptions.IgnoreCase), (ContextBoundObject, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(ChildCare.BuildForm, FormOptions.PromptInStart), AfterChildCare);
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
            //var accountSid = "ACbb85ffd271203038fea0db9c0d06d8c6"; // Your Account SID from www.twilio.com/console
            //var authToken = "39880d052d1a2048f321d0725dc6d6ae";  // Your Auth Token from www.twilio.com/console

            //var twilio = new TwilioRestClient(accountSid, authToken);
            //var message = twilio.SendMessage(
            //    "+17862570432", // From (Replace with your Twilio number)
            //    "7863271555", // To (Replace with your phone number)
            //    "Hello from C#"
            //    );

            return Chain.Return("How can I help you today?  (restaurant reservations, room service or child care) ");
        }

        private async static Task<IDialog<string>> AfterRestaurant(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var rest = token as RestaurantReservation;

            var message = $"Restaurant: {rest.Restaurant}, Number of people: {rest.NumberOfPeople}, Date and time: {rest.DateAndTime} ";
            SendToDashBoard("https://bco1dashboard.fwd.wf/api/restaurant_reservation", message);
            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }

        private async static Task<IDialog<string>> AfterRoomService(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            SendToDashBoard("https://bco1dashboard.fwd.wf/api/room_service", "room service");
            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }

        private async static Task<IDialog<string>> AfterChildCare(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            SendToDashBoard("https://bco1dashboard.fwd.wf/api/child_care", "child care");
            return Chain.Return("Thank you. How can I continue helping? (restaurant reservations, room service or child care)");
        }

        private static void SendToDashBoard(string url, string message)
        {
            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.UploadString(new Uri(url), "POST", json_serializer.Serialize(new DashboardMessage { text = message }));
            }

        }

        private class DashboardMessage
        {
            public string text { get; set; }
        }
    }
}