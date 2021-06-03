using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace MyIdentity.utils
{
  public static class UtilsE
    {
        internal static string HashPassword(string password)
        {
            byte[] salt;
            byte[] bytes;
            if (password == null)
            {
                throw new ArgumentNullException("password or email");
            }
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }

        public static string GetCookie(string id_user)
        {
            return $"assa={id_user}; max-age=3600000";
        }

       //private static void SetEncryptedCookie(string name, string value)
       //{
       //    var encryptName = SomeEncryptionMethod(name);
       //    Response.Cookies[encryptName].Value = SomeEncryptionMethod(value);
       //    //set other cookie properties here, expiry &c.
       //    //Response.Cookies[encryptName].Expires = ...
       //}
       //
       //private static string GetEncryptedCookie(string name)
       //{
       //    //you'll want some checks/exception handling around this
       //    return SomeDecryptionMethod(
       //        Response.Cookies[SomeDecryptionMethod(name)].Value);
       //}
    }
}
