﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace authcontroller.Migrations
{
    [Migration(1)]
    public class _001_UsersAndRoles:Migration
    {

        public override void Down()
        {
           
            Delete.Table("users"); 
           
            
        }

        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("username").AsString(128)
                .WithColumn("email").AsString(256)
                .WithColumn("password_hash").AsString(128);

           

        }
    }
}