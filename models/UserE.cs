using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace MyIdentity.models
{
    [MapTableName("user")]
   public class UserE
    {
        [MapPrimaryKey("id",Generator.Assigned)]
        public Guid IdUser { get; set; }=Guid.NewGuid();
        [MapColumnName("email")]
        public string Email { get; set; }
        [MapColumnName("email_c")]
        public bool ConfirmEmail { get; set; }
        [MapColumnName("telephone")]
        public string Telephone { get; set; }
        [MapColumnName("telephone_c")]
        public bool ConfirmTelephone { get; set; }
        [MapColumnName("password")]
        public string PasswordHash { get; set; }
        [MapColumnName("user_name")]
        public string UserName { get; set; }
        [MapColumnName("avatar")]
        public string AvatarFile { get; set; }
        [MapColumnType("TEXT")]
        [MapColumnName("user_role")]
        public List<string> UserRole { get; set; }=new List<string>();
        [MapColumnType("TEXT")]
        [MapColumnName("user_group")]
        public List<string> UserGroup { get; set; } = new List<string>();
        [MapColumnType("TEXT")]
        [MapColumnName("session")]
        public string Session { get; set; }
    }
}
