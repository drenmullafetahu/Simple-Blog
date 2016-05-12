using authcontroller.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authcontroller
{
    public class Database
    {
        private static ISessionFactory _sessionFactory;
        private static string SessionKey = "SimpleBlogApplication";


        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        { //configure NHbibernate for connecting db

            var config = new Configuration();
            config.Configure();


            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<PostMap>();
            mapper.AddMapping<TagMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            _sessionFactory = config.BuildSessionFactory();



        }


        public static void OpenSession() //invoked at the begining of every request
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()//invoked at the end of every request
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;

                if (session!=null){
                    session.Close();

                }

                HttpContext.Current.Items.Remove(SessionKey);
        }

    }
}