using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyIdentity.models;
using MyIdentity.utils;

namespace MyIdentity
{
    public static class MyHelp
    {

        public static void ActivateE(this IApplicationBuilder app, Action<Starter> setupAction )
        {
            Starter  starter=new Starter();
            setupAction.Invoke(starter); 
            app.UseRouting();
            app.UseMiddleware<MerryService>(starter);
            app.UseEndpoints(endpoints => { endpoints.MapHub<MerryDays>("/chat"); });
        }
       

        public static UserE GetMyUser(HttpContext context)
        {
            var messageSender = context.RequestServices.GetService(typeof(MyProvider));
            var aaa = (IServiceProvider) messageSender;
            MerryService sasas = (MerryService) aaa.GetService(typeof(MerryService));
            return new UserE();
        }
        public static UserE GetUserFromEmail(this HttpContext connection,string email)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            return MerryService.MyMerryService.GetUserFromEmail(email);
        }
        public static UserE GetUserFromIp(this HttpContext connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var requesrtCookie = connection.Request.Cookies["assa"];
            if (requesrtCookie == null) return null;
            return MerryService.MyMerryService.GetUserFromIp(requesrtCookie);
        }

        public static List<UserE> GetAllUser(this HttpContext connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            return MerryService.MyMerryService.GetAllUser();
        }
        public static int SaveUser(this HttpContext connection,UserE user)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            return MerryService.MyMerryService.SaveUser(user);

        }
    }
}