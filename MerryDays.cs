using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MyIdentity.models;
using Newtonsoft.Json;

namespace MyIdentity
{
    public class MerryDays : Hub
    {
        public async Task Send(string message, string userName)
        {
            try
            {
                await Clients.All.SendAsync("Send", ResourceMy.login1, userName);
            }
            catch (Exception e)
            {
                await Clients.Caller.SendAsync("Send", e.ToString(), userName);
            }
        }

        public async Task GetHtml(string message)
        {
            switch (message)
            {
                case "/tt":
                {
                    var asss = Context.GetHttpContext().Request.Cookies;
                    if (Context.GetHttpContext().Request.Cookies.ContainsKey("assa"))
                    {
                        var sd = Context.GetHttpContext().Request.Cookies["assa"];
                    }
                    await Clients.Caller.SendAsync("Send", ResourceMy.login1, ""); 
                    break;
                }
            }
        }

        public async Task CreateUser(string userEmail,string userPasswoerd)
        {
            var user = new UserE {Email = userEmail, PasswordHash = userPasswoerd};
            int sd = Context.GetHttpContext().SaveUser(user);

            if ( sd== 1)
            {
                var r=JsonConvert.SerializeObject(new TempMessage()
                    { cookie = utils.UtilsE.GetCookie(user.IdUser.ToString()), body = ResourceMy.good_registration});
                await Clients.Caller.SendAsync("OkRegister", r );
            }
            else
            { 
                
                await Clients.Caller.SendAsync("CreateUser", "Пользоваетель с такой почтой уже существует.", "");

            }
          
           
        }
    }

   public class TempMessage
    {
        public string body { get; set; }
        public string cookie { get; set; }
    }
}