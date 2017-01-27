using BotClient;
using BotClient.Models;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Bot.Connector.DirectLine.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace DirectLine.Controllers
{
    public class HomeController : Controller
    {

        #region public async Task<ActionResult> Index()
        public async Task<ActionResult> Index(string ChatMessage)
        {

            Chat objChat = new Chat();
            var oldResponse = "";
            if (System.Web.HttpContext.Current.Session["ChatResponse"] != null)
            {
                oldResponse = System.Web.HttpContext.Current.Session["ChatResponse"].ToString();
            }

            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                System.Web.HttpContext.Current.Session["User"] = "bco1" + Guid.NewGuid().ToString();
            }
            if (!String.IsNullOrEmpty(ChatMessage) && ChatMessage.Length > 0)
            {
                var talker = new BotTalker();
                objChat = await talker.TalkToTheBot(ChatMessage);

                objChat.ChatResponse = oldResponse + "<b>" + ChatMessage + "</b></br></br>" + objChat.ChatResponse + "</br>";
                System.Web.HttpContext.Current.Session["ChatResponse"] = objChat.ChatResponse;
            }
            ModelState.Clear();

            return View(new Chat { ChatResponse = objChat.ChatResponse});
        }

        public async Task<ActionResult> Travel(string ChatMessage)
        {

            Chat objChat = new Chat();
            var oldResponse = "";
            if (System.Web.HttpContext.Current.Session["ChatResponse"] != null)
            {
                oldResponse = System.Web.HttpContext.Current.Session["ChatResponse"].ToString();
            }

            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                System.Web.HttpContext.Current.Session["User"] = "bco1"+Guid.NewGuid().ToString();
            }
            if (!String.IsNullOrEmpty(ChatMessage) && ChatMessage.Length > 0)
            {
                var talker = new BotTalker();
                objChat = await talker.TalkToTheBot(ChatMessage);

                objChat.ChatResponse = oldResponse + "<b>" + ChatMessage + "</b></br></br>" + objChat.ChatResponse + "</br>";
                System.Web.HttpContext.Current.Session["ChatResponse"] = objChat.ChatResponse;
            }
            ModelState.Clear();

            return View("~/Views/Home/Index.cshtml",new Chat { ChatResponse = objChat.ChatResponse });
        }
        #endregion

    }
}