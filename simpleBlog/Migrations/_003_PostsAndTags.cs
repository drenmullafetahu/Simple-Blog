using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace authcontroller.Migrations
{
    [Migration(3)]
    public class _003_PostsAndTags:Migration
    {

        public override void Down()
        {
            Delete.Table("post_tags");
            Delete.Table("posts");
            Delete.Table("tags");
        }

        public override void Up()
        {
            Create.Table("posts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id")
                .WithColumn("title").AsString(128)
                .WithColumn("slug").AsString(128)
                .WithColumn("content").AsCustom("TEXT")
                .WithColumn("created_at").AsDateTime()
                .WithColumn("updated_at").AsDateTime().Nullable()
                .WithColumn("deleted_at").AsDateTime().Nullable();



            Create.Table("tags")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(128)
                .WithColumn("slug").AsString(128);

            Create.Table("post_tags")
                .WithColumn("tag_id").AsInt32().ForeignKey("tags", "id").OnDelete(Rule.Cascade)
                .WithColumn("post_id").AsInt32().ForeignKey("posts", "id").OnDelete(Rule.Cascade);

                

        }
    }
}