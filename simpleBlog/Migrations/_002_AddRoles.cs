using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace authcontroller.Migrations
{
    [Migration(2)]
    public class _002_AddRoles:Migration
    {

        public override void Down()
        {
            Delete.Table("role_users");
            Delete.Table("roles");
        }

        public override void Up()
        {
            Create.Table("roles")
               .WithColumn("id").AsInt32().Identity().PrimaryKey()
               .WithColumn("name").AsString(128);

            Create.Table("role_users")
               .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade)
               .WithColumn("role_id").AsInt32().ForeignKey("roles", "id").OnDelete(Rule.Cascade);
        }
    }
}