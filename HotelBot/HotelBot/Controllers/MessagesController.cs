using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using HotelBot.Dialogs;
using HotelBot.Models;
using Twilio;

namespace HotelBot
{
   
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //if (activity.From != null && String.IsNullOrEmpty(activity.From.Id))
                //    {
                //    var accountSid = "ACbb85ffd271203038fea0db9c0d06d8c6"; // Your Account SID from www.twilio.com/console
                //    var authToken = "39880d052d1a2048f321d0725dc6d6ae";  // Your Auth Token from www.twilio.com/console

                //    var twilio = new TwilioRestClient(accountSid, authToken);
                //    var message = twilio.SendMessage(
                //        "+17862570432", // From (Replace with your Twilio number)
                //        "7863271555", // To (Replace with your phone number)
                //        activity.From.Name
                //        );
                //}
                var cruiseSearch = true;

                if (activity.From != null && !String.IsNullOrEmpty(activity.From.Name) && activity.From.Name.ToLower().Contains("bco1"))
                {
                    cruiseSearch = false;
                }
                if (cruiseSearch)
                {
                    if (activity.From != null && !String.IsNullOrEmpty(activity.From.Id) && activity.From.Id.ToLower().Contains("bco1"))
                    {
                        cruiseSearch = false;
                    }
                }

                if (cruiseSearch)
                {
                    await Conversation.SendAsync(activity, () => CruiseSearchDialog.dialog);
                }
                else
                {
                    await Conversation.SendAsync(activity, () => HotelBotDialog.dialog);
                }
                //await Conversation.SendAsync(activity, MakeLuisDialog);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private IDialog<RestaurantReservation> MakeLuisDialog()
        {
            return Chain.From(() => new MyLuisDialog(RestaurantReservation.BuildForm));
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}