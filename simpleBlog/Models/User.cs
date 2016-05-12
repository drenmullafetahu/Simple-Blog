using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authcontroller.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string  PasswordHas { get; set; }
        public virtual IList<Role> Roles { get; set; }


        public virtual void setPassword(string password)
        {
            PasswordHas = BCrypt.Net.BCrypt.HashPassword(password, 13);
        }

        public virtual void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", 13);
        }



        public virtual bool CheckPassword(string password) {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHas);
        }

       
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.UserName, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
                Property(x => x.PasswordHas,  x=> 
                {
                    x.Column("password_hash");
                    x.NotNullable(true);
                }
             );

                Bag(x => x.Roles,

                    x => { 
                        x.Table("role_users"); 
                        x.Key(k => k.Column("user_id"));
                    },
                    x=>  x.ManyToMany(k=>k.Column("role_id"))
                );

        }
    }

}