using BotClient;
using BotClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/chat")]
    public class ChatController : ApiController
    {

        [Route("post")]
        public async Task<Chat> Post(ChatMessage message)
        {
            System.Web.HttpContext.Current.Session["User"] = "ramon";
            var talker = new BotTalker();
            //var objChat = await talker.TalkToTheBot(message.Text);

            //var response = new ChatResponse { Text = objChat.ChatResponse };
         
           return await talker.TalkToTheBot(message.Text);
        }

    }
}
