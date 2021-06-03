using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIdentity.models;
using ORM_1_21_;

namespace MyIdentity.utils
{
   public class Starter
    {
        public string ConnectionString { get; set; }
        public string UrlBaskgroundImage { get; set; }

        
    }

   internal static class InnerStarter
   {
       public static void Start(string cs)
       {
           try
           {
               new Configure(cs, ProviderName.Postgresql, null);
               using (var db = Configure.GetSession())
               {
                   if (db.TableExists<UserE>() == false)
                   {
                       db.TableCreate<UserE>();
                   }
               }
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               throw;
           }
          
       }
   }
}
