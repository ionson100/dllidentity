using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using MyIdentity.models;
using MyIdentity.utils;
using ORM_1_21_;


namespace MyIdentity
{
    public interface IMerryUser
    {
        public UserE GetUserFromEmail(string email);
        public UserE GetUserFromIp(string email);
        public List<UserE> GetAllUser();


        public int SaveUser(UserE user);
    }

    public class MerryService : IMerryUser
    {
        public static MerryService MyMerryService;
        private static readonly string bi = $"style=\"background-image: url('###'); width: 100%; height: 100%;\"";
        private readonly Starter _starter;
        private readonly RequestDelegate _dDelegate;
        public int addin;


        public MerryService(RequestDelegate dDelegate, object o = null)
        {
            MyMerryService = this;
            _dDelegate = dDelegate;
            if (o == null || o is Starter == false)
            {
                throw new ArgumentException("Отсутствует неоходимый агрумент");
            }

            _starter = (Starter) o;
            InnerStarter.Start(_starter.ConnectionString);
        }


        public async Task InvokeAsync(HttpContext context)
        {
            string res = context.Request.Path.Value.ToLower();
            switch (res)
            {
                case "/tt":
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    string sd = ResourceMy.login.Replace("###image1###",ResourceMy.image);
                    if (_starter.UrlBaskgroundImage != null)
                    {
                        sd = sd.Replace("data-ion100=\"100\"", bi.Replace("###", _starter.UrlBaskgroundImage));
                    }

                    await context.Response.WriteAsync(sd);
                    break;
                }
                default:
                {
                    await _dDelegate.Invoke(context);
                    break;
                }
            }
        }


        public UserE GetUserFromEmail(string email)
        {
            throw new NotImplementedException();
        }
        public UserE GetUserFromIp(string guid)
        {
            try
            {
                using var db = Configure.GetSession();
                Guid userId=new Guid(guid);
                var s = db.QuerionAsync<UserE>().Result.SingleOrDefault(a => a.IdUser ==userId );
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int SaveUser(UserE user)
        {
            try
            {
                using (var db = Configure.GetSession())
                {
                    var s = db.QuerionAsync<UserE>().Result.SingleOrDefault(a => a.Email == user.Email);
                    if (s != null)
                    {
                        return -1;
                    }
                    return db.Save(user);
                }
                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<UserE> GetAllUser()
        {
            try
            {
                using (var db = Configure.GetSession())
                {
                    var s = db.QuerionAsync<UserE>().Result.ToList();
                    return s;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class MyProvider : IServiceProvider
    {
        public object? GetService(Type serviceType)
        {
            return MerryService.MyMerryService;
        }
    }
}